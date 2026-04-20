using Application.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Expenses.Request
{
        public class ExpenseDashboardRequest: PageRequest
        {

            public DateTimeOffset? StartDate { get; set; }
            public DateTimeOffset? EndDate { get; set; }
            public CategoryEnum? Category { get; set; }
            public string? SearchText { get; set; }
        }
}
