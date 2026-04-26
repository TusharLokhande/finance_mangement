using API.Extensions;
using Application.Features.Expenses.Intefaces;
using Application.Features.Expenses.Request;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/expense")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _ExpenseService;

        public ExpenseController(
              IExpenseService ExpenseService
            )
        {
            _ExpenseService = ExpenseService;
        }


        [HttpPost]
        public async Task<IActionResult> InsertExpense([FromBody] ExpenseDto request)
        {
            var result = await _ExpenseService.Insert(request);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyExpense([FromBody] ExpenseDto request, Guid id)
        {
            var result =await _ExpenseService.Modify(request, id);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var result =await _ExpenseService.Delete(id);
            return result.ToActionResult();
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> Dashboard([FromBody] ExpenseDashboardRequest req)
        {
            var result =await _ExpenseService.GetDashboard(req);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseDisplay(Guid id)
        {
            var result = await _ExpenseService.GetExpenseForDisplay(id);
            return result.ToActionResult();
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDisplayStats([FromQuery] DateTimeOffset? startDate, DateTimeOffset? endDate, CategoryEnum? category)
        {
            var result = await _ExpenseService.GetExpenseStatsForDisplay(startDate, endDate, category);
            return result.ToActionResult();
        }
    }
}
