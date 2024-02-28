using HotChoco.Dapper.API.Data;
using HotChoco.Dapper.API.Services;
using HotChocolate.Types;

namespace HotChoco.Dapper.API.QueryResolver
{
    [ExtendObjectType("Query")]
    public class UserQueryResolver
    {
        public Task<User> GetUser([Service] IUserService userService, int id)
        {
            return userService.GetUser(id);
        }
        public Task<List<User>> GetAllUsers([Service] IUserService userService)
        {
            return userService.GetAllUsers();
        }
        public Task<List<User>> SearchUsers([Service] IUserService userService, string searchTerm)
        {
            return userService.SearchUsers(searchTerm);
        }
    }
}
