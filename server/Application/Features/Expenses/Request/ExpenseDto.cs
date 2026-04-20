using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Expenses.Request
{
    public record ExpenseDto
    {
        public decimal Amount { get; set; }
        public CategoryEnum Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public PaymentTypeEnum Payment { get; set; }
        public HashSet<string> Tags { get; set; } = new HashSet<string>();
    }
}
