using Application.Common;
using Application.Enums;
using Application.Features.Expenses.Intefaces;
using Application.Features.Expenses.Request;
using Application.Features.Expenses.Response;
using Application.Interfaces;
using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Features.Expenses.Services
{
    public class ExpenseService : IExpenseService
    {

        private readonly IExpenseRepository _ExpenseRepository;
        private readonly IUnitOfWork _Uow;
        private readonly ITagRepository _TagRepository;
        private readonly ILogger<ExpenseService> _Logger;
        private readonly ICurrentUserService _currentUser;

        public ExpenseService(
                IExpenseRepository ExpenseRepository,
                IUnitOfWork uow,
                ITagRepository tagRepository,
                ILogger<ExpenseService> logger,
                ICurrentUserService currentUser
            )
        {
            _ExpenseRepository = ExpenseRepository;
            _Uow = uow;
            _TagRepository = tagRepository;
            _Logger = logger;
            _currentUser = currentUser;
        }


        public async Task<Result<Guid>> Insert(ExpenseDto request)
        {
            try
            {

                var TagIds = _TagRepository.InsertTag(request.Tags);
                var UserId = await _currentUser.GetUserIdAsync();

                ExpensesEntity expense = new ExpensesEntity(
                    request.Amount,
                    request.Category,
                    request.Date,
                    request.Description,
                    request.Payment,
                    UserId
                );

                expense.AddTags(TagIds);
                await _ExpenseRepository.AddAsync(expense);
                await _Uow.SaveChangesAsync();
                return Result<Guid>.Success(expense.Id, "Expense created successfully.");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while inserting the query.");
                throw;
            }
        }

        public async Task<Result<Guid>> Delete(Guid id)
        {
            var expense = await _ExpenseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (expense == null)
                return Result<Guid>.Failure("Expense not found!", ErrorStatus.NotFound);

            await _ExpenseRepository.DeleteAsync(expense);

            await _Uow.SaveChangesAsync();
            return Result<Guid>.Success(id, "Expense Delete Successfully.");
        }

        public async Task<Result<Guid>> Modify(ExpenseDto request, Guid Id)
        {

            var TagIds = _TagRepository.InsertTag(request.Tags);

            var expense = await _ExpenseRepository.GetExpenseToModify(Id);
            if (expense == null)
                return Result<Guid>.Failure("Expense not found!", ErrorStatus.NotFound);


            expense.Modify(
                    request.Amount,
                    request.Category,
                    request.Date,
                    request.Description,
                    request.Payment
            );

            expense.AddTags(TagIds);
            await _ExpenseRepository.UpdateAsync(expense);
            await _Uow.SaveChangesAsync();
            return Result<Guid>.Success(Id, "Expense Modified Successfully.");
        }


        public async Task<Result<PagedResult<ExpenseDashboardDto>>> GetDashboard(ExpenseDashboardRequest request)
        {
            var data = await _ExpenseRepository.GetDashboardAsync(request);
            return Result<PagedResult<ExpenseDashboardDto>>.Success(data);
        }

        public async Task<Result<DisplayExpense?>> GetExpenseForDisplay(Guid Id)
        {
            var data = await _ExpenseRepository.GetDisplayExpense(Id);
            return Result<DisplayExpense?>.Success(data);
        }

        public async Task<Result<ExpenseStatsDto>> GetExpenseStatsForDisplay(DateTimeOffset? startDate, DateTimeOffset? endDate, CategoryEnum? category)
        {
            var data = await _ExpenseRepository.GetExpenseStats(startDate, endDate, category);
            return Result<ExpenseStatsDto>.Success(data);
        }
    }
}
