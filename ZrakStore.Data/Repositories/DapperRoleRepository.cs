using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Data.Utilities;

namespace ZrakStore.Data.Repositories
{
    public class DapperRoleRepository : IAsyncRoleRepository
    {
        private readonly DapperConnectionString connectionString;

        public DapperRoleRepository(DapperConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task AddAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task AddToRoleAsync(string userId, string roleId)
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var affectedRows = await dbConnection.ExecuteAsync("spUserRoles_Add @UserId, @RoleId", new { UserId = userId, RoleId = roleId });
            }
        }

        public async Task<IEnumerable<string>> GetRolesByUsernameAsync(string username)
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var roles = await dbConnection.QueryAsync<string>("spGetRolesByUsername @Username", new { Username = username });
                return roles;
            }
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Role t)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Role t)
        {
            throw new NotImplementedException();
        }
    }
}
