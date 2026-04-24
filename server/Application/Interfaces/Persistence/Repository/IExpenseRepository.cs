using Application.Common;
using Application.Features.Expenses.Request;
using Application.Features.Expenses.Response;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence.Repository
{
    public interface IExpenseRepository: IGenericRepository<ExpensesEntity>
    {
        Task<PagedResult<ExpenseDashboardDto>> GetDashboardAsync(ExpenseDashboardRequest request);

        Task<DisplayExpense?> GetDisplayExpense(Guid Id);

        Task<ExpensesEntity> GetExpenseToModify(Guid Id);

        Task<ExpenseStatsDto> GetExpenseStats();
    }
}
