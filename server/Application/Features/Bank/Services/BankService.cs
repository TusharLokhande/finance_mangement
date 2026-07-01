using Application.Common;
using Application.Enums;
using Application.Features.Bank.Interfaces;
using Application.Features.Bank.Request;
using Application.Features.Bank.Response;
using Application.Interfaces;
using Application.Interfaces.Persistence.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Bank.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _BankRepository;
        private readonly IUnitOfWork _Uow;
        private readonly ILogger<BankService> _Logger;
        private readonly ICurrentUserService _CurrentUser;

        public BankService(
            IBankRepository bankRepository,
            IUnitOfWork uow,
            ILogger<BankService> logger,
            ICurrentUserService currentUser
        )
        {
            _BankRepository = bankRepository;
            _Uow = uow;
            _Logger = logger;
            _CurrentUser = currentUser;
        }

        public async Task<Result<BankDto>> CreateBankAsync(CreateBankDto request)
        {
            try
            {
                var userId = await _CurrentUser.GetUserIdAsync();

                var bankAccount = new AccountEntity(
                    userId,
                    request.AccountNumber,
                    request.BankName
                );

                await _BankRepository.AddAsync(bankAccount);
                await _Uow.SaveChangesAsync();

                return Result<BankDto>.Success(
                    new BankDto
                    {
                        Id = bankAccount.Id,
                        AccountNumber = bankAccount.AccountNumber,
                        BankName = bankAccount.BankName,
                        IsActive = bankAccount.IsActive,
                        CreatedAt = bankAccount.CreatedAt
                    },
                    "Bank account created successfully."
                );
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while creating bank account.");
                throw;
            }
        }

        public async Task<Result<BankDto>> UpdateBankAsync(UpdateBankDto request)
        {
            try
            {
                var userId = await _CurrentUser.GetUserIdAsync();
                var bankAccount = await _BankRepository.GetUserBankByIdAsync(userId, request.Id);

                if (bankAccount == null)
                {
                    return Result<BankDto>.Failure("Bank account not found!", ErrorStatus.NotFound);
                }

                bankAccount.AccountNumber = request.AccountNumber;
                bankAccount.BankName = request.BankName;

                await _BankRepository.UpdateAsync(bankAccount);
                await _Uow.SaveChangesAsync();

                return Result<BankDto>.Success(
                    new BankDto
                    {
                        Id = bankAccount.Id,
                        AccountNumber = bankAccount.AccountNumber,
                        BankName = bankAccount.BankName,
                        IsActive = bankAccount.IsActive,
                        CreatedAt = bankAccount.CreatedAt
                    },
                    "Bank account updated successfully."
                );
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while updating bank account.");
                throw;
            }
        }

        public async Task<Result<Guid>> DeleteBankAsync(Guid id)
        {
            try
            {
                var userId = await _CurrentUser.GetUserIdAsync();
                var bankAccount = await _BankRepository.GetUserBankByIdAsync(userId, id);

                if (bankAccount == null)
                {
                    return Result<Guid>.Failure("Bank account not found!", ErrorStatus.NotFound);
                }

                bankAccount.IsActive = false;
                await _BankRepository.UpdateAsync(bankAccount);
                await _Uow.SaveChangesAsync();

                return Result<Guid>.Success(id, "Bank account deleted successfully.");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while deleting bank account.");
                throw;
            }
        }

        public async Task<Result<BankDto>> GetBankByIdAsync(Guid id)
        {
            try
            {
                var userId = await _CurrentUser.GetUserIdAsync();
                var bankAccount = await _BankRepository.GetUserBankByIdAsync(userId, id);

                if (bankAccount == null)
                {
                    return Result<BankDto>.Failure("Bank account not found!", ErrorStatus.NotFound);
                }

                return Result<BankDto>.Success(
                    new BankDto
                    {
                        Id = bankAccount.Id,
                        AccountNumber = bankAccount.AccountNumber,
                        BankName = bankAccount.BankName,
                        IsActive = bankAccount.IsActive,
                        CreatedAt = bankAccount.CreatedAt
                    }
                );
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while retrieving bank account.");
                throw;
            }
        }

        public async Task<Result<IEnumerable<BankListDto>>> GetAllBanksAsync()
        {
            try
            {
                var userId = await _CurrentUser.GetUserIdAsync();
                var bankAccounts = await _BankRepository.GetUserBanksAsync(userId);

                var bankList = bankAccounts.Select(b => new BankListDto
                {
                    Id = b.Id,
                    AccountNumber = b.AccountNumber,
                    BankName = b.BankName,
                    IsActive = b.IsActive
                }).ToList();

                return Result<IEnumerable<BankListDto>>.Success(bankList);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error while retrieving bank accounts.");
                throw;
            }
        }
    }
}
