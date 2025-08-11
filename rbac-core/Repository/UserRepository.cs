using Microsoft.EntityFrameworkCore;
using rbac_core.Context;
using rbac_core.Entity;
using rbac_core.Interface.Repository;
using rbac_core.Views;

namespace rbac_core.Repository
{
    public sealed class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Profile> AddProfile(Profile profile)
        {
            await _dbContext.Profiles.AddAsync(profile);
            await _dbContext.SaveChangesAsync();

            return profile;
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _dbContext
                .Users.Include(u => u.Profile)
                .SingleOrDefaultAsync(u => u.ID == id);

            if (user?.Profile is not null)
            {
                _dbContext.Remove(user.Profile);
            }

            if (user is not null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUser(string email)
        {
            return await _dbContext
                .Users.Include(u => u.Profile)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserFromID(int id)
        {
            return await _dbContext
                .Users.Include(u => u.Profile)
                .SingleOrDefaultAsync(u => u.ID == id);
        }

        public (int, List<UserResponse>) ListUsers(PaginatedRequest<UserListRequest> req)
        {
            var query = _dbContext.Users.Include(u => u.Profile).AsQueryable();

            if (req.Filters?.Email is not null)
            {
                query = query.Where(u => u.Email.Contains(req.Filters.Email));
            }

            if (req.Filters?.Role is not null)
            {
                var role = req.Filters.Role.GetValueOrDefault().ToString();
                query = query.Where(u => u.Roles.Contains(role));
            }

            if (req.Filters?.FirstName is not null)
            {
                query = query.Where(u =>
                    u.Profile != null && u.Profile.FirstName.Contains(req.Filters.FirstName)
                );
            }

            if (req.Filters?.LastName is not null)
            {
                query = query.Where(u =>
                    u.Profile != null && u.Profile.LastName.Contains(req.Filters.LastName)
                );
            }

            int count = query.Count();

            if (req.PageRequest.AllRecords != true)
            {
                int skip = (req.PageRequest.PageNumber - 1) * req.PageRequest.PageSize;
                query = query.Skip(skip).Take(req.PageRequest.PageSize);
            }

            var list = query
                .Select(u => new UserResponse
                {
                    ID = u.ID,
                    Email = u.Email,
                    FirstName = u.Profile != null ? u.Profile.FirstName : null,
                    LastName = u.Profile != null ? u.Profile.LastName : null,
                    Address = u.Profile != null ? u.Profile.Address : null,
                    Phone = u.Profile != null ? u.Profile.Phone : null,
                    Roles =
                        u.Roles != null
                            ? u.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            : Array.Empty<string>(),
                })
                .ToList();

            return (count, list);
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);

            if (user.Profile != null)
            {
                _dbContext.Profiles.Update(user.Profile);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}
