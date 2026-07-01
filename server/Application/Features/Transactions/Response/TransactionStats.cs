using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Features.Transactions.Response
{
    public class TransactionStatsDtoRaw
    {
        public decimal? TotalThisMonth { get; set; }
        public decimal? Weekly { get; set; }
        public int? TopCategory { get; set; }
        public decimal? Avg { get; set; }
        public decimal? PrevMonthTotal { get; set; }
    }

    public class TransactionStatsDto : TransactionStatsDtoRaw
    {
        public decimal PercentChange { get; set; }
    }
}
