using Application.Common;
using Application.Features.Expenses.Request;
using Application.Features.Expenses.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Expenses.Intefaces
{
    public interface IExpenseService
    {
        Task<Result<Guid>> Insert(ExpenseDto request);

        Task<Result<Guid>> Delete(Guid id);

        Task<Result<Guid>> Modify(ExpenseDto request, Guid Id);

        Task<Result<PagedResult<ExpenseDashboardDto>>> GetDashboard(ExpenseDashboardRequest request);

        Task<Result<DisplayExpense?>> GetExpenseForDisplay(Guid Id);

        Task<Result<ExpenseStatsDto>> GetExpenseStatsForDisplay();
    }
}
