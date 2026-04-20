using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Expenses.Response
{

    public class ExpenseDashboardDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public CategoryEnum Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<string> Tags { get; set; }
        public PaymentTypeEnum Payment { get; set; }
    }
}
