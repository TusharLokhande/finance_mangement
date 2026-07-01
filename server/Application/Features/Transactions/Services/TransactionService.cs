using Application.Common;
using Application.Enums;
using Application.Features.Transactions.Intefaces;
using Application.Features.Transactions.Request;
using Application.Features.Transactions.Response;
using Application.Interfaces;
using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Features.Transactions.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionRepository _ExpenseRepository;
        private readonly IUnitOfWork _Uow;
        private readonly ITagRepository _TagRepository;
        private readonly ILogger<TransactionService> _Logger;
        private readonly ICurrentUserService _currentUser;

        public TransactionService(
                ITransactionRepository TransactionRepository,
                IUnitOfWork uow,
                ITagRepository tagRepository,
                ILogger<TransactionService> logger,
                ICurrentUserService currentUser
            )
        {
            _ExpenseRepository = TransactionRepository;
            _Uow = uow;
            _TagRepository = tagRepository;
            _Logger = logger;
            _currentUser = currentUser;
        }


        public async Task<Result<Guid>> Insert(TransactionDto request)
        {
            try
            {

                var TagIds = _TagRepository.InsertTag(request.Tags);
                var UserId = await _currentUser.GetUserIdAsync();

                TransactionEntity trasaction = new(
                    request.Amount,
                    request.Category,
                    request.Date,
                    request.Description,
                    request.TransactionType,
                    request.BankAccount,
                    request.LentTo,
                    request.LentStatus,
                    UserId
                );

                trasaction.AddTags(TagIds);
                await _ExpenseRepository.AddAsync(trasaction);
                await _Uow.SaveChangesAsync();
                return Result<Guid>.Success(trasaction.Id, "Expense created successfully.");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while inserting the query.");
                throw;
            }
        }

        public async Task<Result<Guid>> Delete(Guid id)
        {
            var transaction = await _ExpenseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (transaction == null)
                return Result<Guid>.Failure("Expense not found!", ErrorStatus.NotFound);

            await _ExpenseRepository.DeleteAsync(transaction);

            await _Uow.SaveChangesAsync();
            return Result<Guid>.Success(id, "Expense Delete Successfully.");
        }

        public async Task<Result<Guid>> Modify(TransactionDto request, Guid Id)
        {

            var TagIds = _TagRepository.InsertTag(request.Tags);

            var transaction = await _ExpenseRepository.GetExpenseToModify(Id);
            if (transaction == null)
                return Result<Guid>.Failure("Expense not found!", ErrorStatus.NotFound);


            transaction.Modify(
                    request.Amount,
                    request.Category,
                    request.Date,
                    request.Description,
                    request.TransactionType,
                    request.BankAccount,
                    request.LentTo,
                    request.LentStatus
            );

            transaction.AddTags(TagIds);
            await _ExpenseRepository.UpdateAsync(transaction);
            await _Uow.SaveChangesAsync();
            return Result<Guid>.Success(Id, "Transaction Modified Successfully.");
        }


        public async Task<Result<PagedResult<TransactionDashboardDto>>> GetDashboard(TransactionDashboardRequest request)
        {
            var data = await _ExpenseRepository.GetDashboardAsync(request);
            return Result<PagedResult<TransactionDashboardDto>>.Success(data);
        }

        public async Task<Result<DisplayTransaction?>> GetTransactionForDisplay(Guid Id)
        {
            var data = await _ExpenseRepository.GetDisplayExpense(Id);
            return Result<DisplayTransaction?>.Success(data);
        }

        public async Task<Result<TransactionStatsDto>> GetTransactionStatsForDisplay(DateTimeOffset? startDate, DateTimeOffset? endDate, CategoryEnum? category)
        {
            var data = await _ExpenseRepository.GetExpenseStats(startDate, endDate, category);
            return Result<TransactionStatsDto>.Success(data);
        }
    }
}
