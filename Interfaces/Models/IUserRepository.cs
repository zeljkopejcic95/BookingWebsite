using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Interfaces.Models;

public interface IUserRepository
{
    Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool trackChanges);
    Task<User> GetUserAsync(int userId, bool trackChanges);
    Task<User> GetUserByEmailAsync(string email);
    void CreateUser(User user);
    void DeleteUser(User user);
}
