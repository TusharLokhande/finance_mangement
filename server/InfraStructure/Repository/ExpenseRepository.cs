using Application.Common;
using Application.Features.Expenses.Request;
using Application.Features.Expenses.Response;
using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository
{
    public class ExpenseRepository : GenericRepository<Expenses>, IExpenseRepository
    {
        private readonly AppDbContext _db;
        public ExpenseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<PagedResult<ExpenseDashboardDto>> GetDashboardAsync(ExpenseDashboardRequest request)
        {
            var query = _db.Expenses
                .AsNoTracking()
                .AsQueryable().Where(k => k.IsActive);

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
                    x.ExpenseTagMapping.Any(m =>
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
                .Select(x => new ExpenseDashboardDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Category = x.Category,
                    Description = x.Description,
                    Date = x.Date,
                    Payment = x.Payment,

                    Tags = x.ExpenseTagMapping
                            .Select(m => m.Tags.TagName)
                            .ToList()
                })
                .ToListAsync();

            return new PagedResult<ExpenseDashboardDto>
            (
                totalCount,
                data,
                request.PageSize
            );
        }


        public async Task<DisplayExpense?> GetDisplayExpense(Guid Id)
        {
            var result = await _db.Expenses.AsNoTracking().Where(k => k.Id == Id)
                    .Select(
                        s => new DisplayExpense()
                        {
                            Amount = s.Amount,
                            Description = s.Description,
                            Date = s.Date,
                            Category = s.Category,
                            Tags = s.ExpenseTagMapping.Select(m => m.Tags.TagName).ToList()
                        }
                     ).FirstOrDefaultAsync();

            return result;
        }


        public async Task<Expenses> GetExpenseToModify(Guid Id)
        {
            var result = await _db.Expenses.Where(k => k.Id == Id).Include(k => k.ExpenseTagMapping).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ExpenseStatsDto> GetExpenseStats()
        {
            var result = await _db.Set<ExpenseStatsDtoRaw>()
            .FromSqlRaw("SELECT * FROM get_expense_stats()")
            .AsNoTracking()
            .FirstAsync();

            var percentChange = result.PrevMonthTotal == 0
                ? 100
                : ((result.PrevMonthTotal ?? (decimal?)0 - result.PrevMonthTotal.Value ?? 0)
                    / result.PrevMonthTotal.Value) * 100;

            return new ExpenseStatsDto
            {
                TotalThisMonth = result.TotalThisMonth ?? 0,
                Weekly = result.Weekly ?? 0,
                Avg = result.Avg ?? 0,
                TopCategory = result.TopCategory,
                PrevMonthTotal=  result.PrevMonthTotal
            };
        }
    }
}
