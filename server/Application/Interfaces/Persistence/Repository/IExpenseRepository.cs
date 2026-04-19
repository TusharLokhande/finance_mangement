using Application.Common;
using Application.Features.Request;
using Application.Features.Response;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence.Repository
{
    public interface IExpenseRepository: IGenericRepository<Expenses>
    {
        Task<PagedResult<ExpenseDashboardDto>> GetDashboardAsync(ExpenseDashboardRequest request);

        Task<DisplayExpense?> GetDisplayExpense(Guid Id);

        Task<Expenses> GetExpenseToModify(Guid Id);

        Task<ExpenseStatsDto> GetExpenseStats();
    }
}
