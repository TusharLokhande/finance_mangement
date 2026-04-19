using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence.Repository
{
    public interface ITagRepository
    {
        public List<Guid> InsertTag(HashSet<string> Tags);
    }
}
