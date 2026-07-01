using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Transactions.Response
{

    public class TransactionDashboardDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public CategoryEnum Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset Date { get; set; }
        public List<string> Tags { get; set; } = [];
        public TransactionType TransactionType { get; set; }
        public Guid BankAccount { get; set; }
        public string? LentTo { get; set; }
        public LentStatus? LentStatus { get; set; }
    }
}
