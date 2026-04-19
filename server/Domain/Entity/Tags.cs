using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Tags: BaseEntity
    {
        public Tags()
        {
            
        }
        public Tags(string TagName)
        {
            this.TagName = TagName;
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string TagName { get; set; }

        public List<ExpenseTagMapping> ExpenseTagMapping { get; set; }
    }
}
