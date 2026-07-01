using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace InfraStructure.Repository
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Users> CreateUserIfNotExists(
            string name,
            string email,
            string azureOid
        )
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.AzureOid == azureOid);

            if (user is not null)
                return user;

            var newUser = new Users
            {
                Name = name,
                Email = email,
                AzureOid = azureOid,
                IsActive = true
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return newUser;
        }
    }
}
