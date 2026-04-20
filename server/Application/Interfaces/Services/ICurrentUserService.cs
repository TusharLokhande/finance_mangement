using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Task<Guid> GetUserIdAsync();
    }
}
