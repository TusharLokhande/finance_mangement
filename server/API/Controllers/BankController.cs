using API.Extensions;
using Application.Features.Bank.Interfaces;
using Application.Features.Bank.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/bank")]
    [ApiController]
    [Authorize]
    public class BankController : ControllerBase
    {
        private readonly IBankService _BankService;

        public BankController(IBankService bankService)
        {
            _BankService = bankService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] CreateBankDto request)
        {
            var result = await _BankService.CreateBankAsync(request);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBank(Guid id)
        {
            var result = await _BankService.GetBankByIdAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            var result = await _BankService.GetAllBanksAsync();
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBank([FromBody] UpdateBankDto request, Guid id)
        {
            request.Id = id;
            var result = await _BankService.UpdateBankAsync(request);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(Guid id)
        {
            var result = await _BankService.DeleteBankAsync(id);
            return result.ToActionResult();
        }
    }
}
