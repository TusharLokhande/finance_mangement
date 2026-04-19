using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class PageRequest
    {
        public int Page { get; set;  }
        public int PageSize { get; set; }   
        public PageSorting? Sorting { get; set; } = new PageSorting();
    }

    public class PageSorting
    {
        public string? Field { get; set; }
        public string? Direction { get; set; }   
    }
}
