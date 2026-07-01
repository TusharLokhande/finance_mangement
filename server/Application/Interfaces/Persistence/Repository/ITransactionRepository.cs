using Application.Common;
using Application.Features.Transactions.Request;
using Application.Features.Transactions.Response;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence.Repository
{
    public interface ITransactionRepository : IGenericRepository<TransactionEntity>
    {
        Task<PagedResult<TransactionDashboardDto>> GetDashboardAsync(TransactionDashboardRequest request);

        Task<DisplayTransaction?> GetDisplayExpense(Guid Id);

        Task<TransactionEntity> GetExpenseToModify(Guid Id);

        Task<TransactionStatsDto> GetExpenseStats(DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, CategoryEnum? category = null);
    }
}
