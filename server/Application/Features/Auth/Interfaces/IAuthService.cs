using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Result<Guid?>> InsertUpdateUser();
    }
}
