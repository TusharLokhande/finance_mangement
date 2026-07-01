using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Tags : BaseEntity
    {
        public Tags() { }
        public Tags(string TagName)
        {
            Id = Guid.NewGuid();
            this.TagName = TagName;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string TagName { get; set; }

        public List<TransactionTagMapping> TransactionTagMapping { get; set; }
    }
}
