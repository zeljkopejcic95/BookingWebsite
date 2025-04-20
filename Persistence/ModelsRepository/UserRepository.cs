using Entities;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.ModelsRepository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<User>> GetAllUsersAsync(
        UserParameters userParameters,
        bool trackChanges)
    {
        var users = FindAll(trackChanges)
        .OrderBy(u => u.Name);

        return await PagedList<User>.ToPagedList(
            users,
            userParameters.PageNumber,
            userParameters.PageSize);
    }

    public async Task<User> GetUserAsync(int userId, bool trackChanges) =>
        await FindByCondition(u => u.Id == userId, trackChanges)
        .SingleOrDefaultAsync();

    public async Task<User> GetUserByEmailAsync(string email) =>
        await FindByCondition(u => u.Email == email, trackChanges: false)
        .FirstOrDefaultAsync();

    public void CreateUser(User user) => Create(user);

    public void DeleteUser(User user) => Delete(user);
}