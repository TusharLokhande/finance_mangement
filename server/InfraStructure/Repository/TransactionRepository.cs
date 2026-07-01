using Application.Common;
using Application.Features.Transactions.Request;
using Application.Features.Transactions.Response;
using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Enums;
using InfraStructure.Extensions;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository
{
    public class TransactionRepository : GenericRepository<TransactionEntity>, ITransactionRepository
    {
        private readonly AppDbContext _db;
        private readonly ICurrentUserService _currentUserService;
        public TransactionRepository(AppDbContext db, ICurrentUserService current) : base(db)
        {
            _db = db;
            _currentUserService = current;
        }

        public async Task<PagedResult<TransactionDashboardDto>> GetDashboardAsync(TransactionDashboardRequest request)
        {
            var UserId = await _currentUserService.GetUserIdAsync();
            var query = _db.Transactions
                .AsNoTracking()
                .AsQueryable().Where(k => k.UserId == UserId && k.IsActive);

            if (request.StartDate.HasValue)
            {
                query = query.Where(x => x.Date.Date >= request.StartDate.Value.Date);
            }

            if (request.EndDate.HasValue)
                query = query.Where(x => x.Date.Date <= request.EndDate.Value.Date);

            if (request.Category.HasValue && request.Category.Value > 0)
            {
                query = query.Where(x => x.Category == request.Category.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var search = $"%{request.SearchText}%";

                query = query.Where(x =>
                    EF.Functions.ILike(x.Description, search) ||
                    x.TransactionsTagMappings.Any(m =>
                        EF.Functions.ILike(m.Tags.TagName, search)
                    )
                );
            }


            query = request.Sorting?.Field?.ToLower() switch
            {
                "amount" => request.Sorting.Direction == "desc"
                    ? query.OrderByDescending(x => x.Amount)
                    : query.OrderBy(x => x.Amount),

                "date" => request.Sorting.Direction == "desc"
                    ? query.OrderByDescending(x => x.Date)
                    : query.OrderBy(x => x.Date),

                "category" => request.Sorting.Direction == "desc"
                    ? query.OrderByDescending(x => x.Category)
                    : query.OrderBy(x => x.Category),

                _ => query.OrderByDescending(k => k.ModifiedAt).OrderByDescending(x => x.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TransactionDashboardDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Category = x.Category,
                    Description = x.Description,
                    Date = x.Date,
                    BankAccount = x.BankAccount,
                    TransactionType = x.TransactionType,
                    LentTo = x.LentTo,
                    LentStatus = x.LentStatus,
                    Tags = x.TransactionsTagMappings
                            .Select(m => m.Tags.TagName)
                            .ToList()
                })
                .ToListAsync();

            return new PagedResult<TransactionDashboardDto>
            (
                totalCount,
                data,
                request.PageSize
            );
        }


        public async Task<DisplayTransaction?> GetDisplayExpense(Guid Id)
        {
            var result = await _db.Transactions.AsNoTracking().Where(k => k.Id == Id)
                    .Select(
                        s => new DisplayTransaction()
                        {
                            Amount = s.Amount,
                            Description = s.Description,
                            Date = s.Date,
                            Category = s.Category,
                            TransactionType = s.TransactionType.GetDescription(),
                            BankAccount = s.BankAccountEntity.BankName,
                            LentTo = s.LentTo,
                            LentStatus = s.LentStatus != null ? s.LentStatus.GetDescription() : null,
                            Tags = s.TransactionsTagMappings.Select(m => m.Tags.TagName).ToList()
                        }
                     ).FirstOrDefaultAsync();

            return result;
        }


        public async Task<TransactionEntity> GetExpenseToModify(Guid Id)
        {
            var result = await _db.Transactions.Where(k => k.Id == Id).Include(k => k.TransactionsTagMappings).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TransactionStatsDto> GetExpenseStats(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, CategoryEnum? category = null)
        {
            string s = startDate.HasValue ? startDate.Value.Date.ToString() : "";
            var result = await _db.Set<TransactionStatsDtoRaw>()
                                .FromSqlInterpolated($@"
                                    SELECT * 
                                    FROM get_expense_stats_v2(
                                        {startDate}, 
                                        {endDate},
                                        {category}
                                    )
                                ")
                                .AsNoTracking()
                                .FirstAsync();

            var percentChange = result.PrevMonthTotal == 0
                ? 100
                : ((result.PrevMonthTotal ?? (decimal?)0 - result.PrevMonthTotal.Value ?? 0)
                    / result.PrevMonthTotal.Value) * 100;

            return new TransactionStatsDto
            {
                TotalThisMonth = result.TotalThisMonth ?? 0,
                Weekly = result.Weekly ?? 0,
                Avg = result.Avg ?? 0,
                TopCategory = result.TopCategory,
                PrevMonthTotal = result.PrevMonthTotal,
                PercentChange = percentChange
            };
        }
    }
}
