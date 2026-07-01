using Application.Common;
using Application.Features.Bank.Request;
using Application.Features.Bank.Response;

namespace Application.Features.Bank.Interfaces
{
    public interface IBankService
    {
        Task<Result<BankDto>> CreateBankAsync(CreateBankDto request);
        Task<Result<BankDto>> UpdateBankAsync(UpdateBankDto request);
        Task<Result<Guid>> DeleteBankAsync(Guid id);
        Task<Result<BankDto>> GetBankByIdAsync(Guid id);
        Task<Result<IEnumerable<BankListDto>>> GetAllBanksAsync();
    }
}
