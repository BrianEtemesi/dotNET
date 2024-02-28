using Dapper;
using HotChoco.Dapper.API.Data;
using System.Data;
using System.Data.SqlClient;

namespace HotChoco.Dapper.API.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task<List<User>> GetAllUsers();
        Task<List<User>> SearchUsers(string searchTerm);
        Task<User> CreateUser(User newUser);
        Task<User> UpdateUser(User updatedUser);
        Task<bool> BulkActivateDeactivateUsers(List<int> userIds, int action);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IDbConnection Connection
        { 
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                using (var connection = Connection)
                {
                    var query = "Select * FROM Users WHERE Id = @UserId";
                    return await connection.QueryFirstOrDefaultAsync<User>(query, new { UserId = id });
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "GetUser");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                using (var connection = Connection)
                {
                    var query = "SELECT * FROM Users ORDER BY Id DESC;";
                    return (await connection.QueryAsync<User>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "GetAllUsers");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
        }


        public async Task<User> CreateUser(User newUser)
        {
            try
            {
                using (var connection = Connection)
                {
                    var query = @"
                    INSERT INTO Users (Name, PhoneNumber, Email, Address, RoleId, DateCreated, DateEdited, Status)
                    VALUES (@Name, @PhoneNumber, @Email, @Address, @RoleId, @DateCreated, @DateEdited, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                    newUser.DateCreated = DateTime.Now;
                    newUser.Status = "Inactive";

                    newUser.Id = await connection.QueryFirstOrDefaultAsync<int>(query, newUser);

                    return newUser;
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "CreateUser");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
            
        }

        public async Task<List<User>> SearchUsers(string searchTerm)
        {
            try
            {
                using (var connection = Connection)
                {
                    var query = @"
                        SELECT *
                        FROM Users
                        WHERE Name LIKE @SearchTerm OR Email LIKE @SearchTerm;";

                    var parameters = new
                    {
                        SearchTerm = $"%{searchTerm}%"
                    };

                    return (await connection.QueryAsync<User>(query, parameters)).ToList();
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "SearchUsers");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        public async Task<User> UpdateUser(User updatedUser)
        {
            try
            {
                using (var connection = Connection)
                {
                    var query = @"
                        UPDATE Users 
                        SET 
                            Name = @Name, 
                            PhoneNumber = @PhoneNumber, 
                            Email = @Email, 
                            Address = @Address, 
                            RoleId = @RoleId, 
                            DateEdited = @DateEdited
                        WHERE Id = @Id;";

                    updatedUser.DateEdited = DateTime.Now;

                    var rowsAffected = await connection.ExecuteAsync(query, updatedUser);

                    if (rowsAffected > 0)
                    {
                        return updatedUser;
                    }
                    else
                    {
                        Console.WriteLine("User not found for update.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "UpdateUser");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
        }
        public async Task<bool> BulkActivateDeactivateUsers(List<int> userIds, int action)
        {
            try
            {
                using (var connection = Connection)
                {
                    var status = action == 1 ? "Active" : "Inactive";

                    var query = $@"
                        UPDATE Users 
                        SET 
                            Status = '{status}'
                        WHERE Id IN @UserIds;";

                    var parameters = new
                    {
                        UserIds = userIds
                    };

                    var rowsAffected = await connection.ExecuteAsync(query, parameters);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString(), "BulkActivateDeactivateUsers");
                throw new Exception(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

    }
}
