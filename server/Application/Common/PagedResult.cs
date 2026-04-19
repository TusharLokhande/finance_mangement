using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(
                int Total,
                List<T>? Data,
                int PageSize
            )
        {

            TotalCount = Total;
            this.Data = Data;
            PageCount = PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
        }

        public int TotalCount { get; set; }
        public List<T>? Data { get; set; }
        public int PageCount { get; set; }
        
    }
}
