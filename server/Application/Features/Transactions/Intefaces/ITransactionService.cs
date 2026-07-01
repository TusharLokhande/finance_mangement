using Application.Common;
using Application.Features.Transactions.Request;
using Application.Features.Transactions.Response;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Transactions.Intefaces
{
    public interface ITransactionService
    {
        Task<Result<Guid>> Insert(TransactionDto request);

        Task<Result<Guid>> Delete(Guid id);

        Task<Result<Guid>> Modify(TransactionDto request, Guid Id);

        Task<Result<PagedResult<TransactionDashboardDto>>> GetDashboard(TransactionDashboardRequest request);

        Task<Result<DisplayTransaction?>> GetTransactionForDisplay(Guid Id);

        Task<Result<TransactionStatsDto>> GetTransactionStatsForDisplay(DateTimeOffset? startDate, DateTimeOffset? endDate, CategoryEnum? category);
    }
}
