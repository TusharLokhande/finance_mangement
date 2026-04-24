using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraStructure.Repository
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }
    }
}
