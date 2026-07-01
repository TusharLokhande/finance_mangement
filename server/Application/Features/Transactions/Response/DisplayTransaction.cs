using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Transactions.Response
{
    public record DisplayTransaction
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public CategoryEnum Category { get; set; }
        public List<string> Tags { get; set; } = [];
        public string TransactionType { get; set; } = string.Empty;
        public string BankAccount { get; set; } = string.Empty;
        public string? LentStatus { get; set; }
        public string? LentTo { get; set; }
    }
}
