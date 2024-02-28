using HotChoco.Dapper.API.Data;
using HotChoco.Dapper.API.Services;

namespace HotChoco.Dapper.API.MutationResolver
{
    [ExtendObjectType("Mutation")]
    public class UserMutationResolver
    {
        public Task<User> CreateUser([Service] IUserService userService, User newUser)
        { 
            return userService.CreateUser(newUser);
        }
        public Task<User> UpdateUser([Service] IUserService userService, User updateUser)
        {
            return userService.UpdateUser(updateUser);
        }
        public Task<bool> ActivateDeactivateUsers([Service] IUserService userService, List<int> userIds, int action)
        {
            return userService.BulkActivateDeactivateUsers(userIds, action);
        }
    }
}
