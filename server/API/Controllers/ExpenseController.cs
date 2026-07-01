using API.Extensions;
using Application.Features.Transactions.Intefaces;
using Application.Features.Transactions.Request;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    [Authorize]
    public class TransactionController(
          ITransactionService _TransactionService
            ) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> InsertTransaction([FromBody] TransactionDto request)
        {
            var result = await _TransactionService.Insert(request);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTransaction([FromBody] TransactionDto request, Guid id)
        {
            var result = await _TransactionService.Modify(request, id);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var result = await _TransactionService.Delete(id);
            return result.ToActionResult();
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> Dashboard([FromBody] TransactionDashboardRequest req)
        {
            var result = await _TransactionService.GetDashboard(req);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionDisplay(Guid id)
        {
            var result = await _TransactionService.GetTransactionForDisplay(id);
            return result.ToActionResult();
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDisplayStats([FromQuery] DateTimeOffset? startDate, DateTimeOffset? endDate, CategoryEnum? category)
        {
            var result = await _TransactionService.GetTransactionStatsForDisplay(startDate, endDate, category);
            return result.ToActionResult();
        }
    }
}
