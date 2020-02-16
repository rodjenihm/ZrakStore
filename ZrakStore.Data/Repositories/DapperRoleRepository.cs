using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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

        public Task AddAsync(Role t)
        {
            throw new NotImplementedException();
        }

        public async Task AddToRoleAsync(string userId, string roleId)
        {
            var sql = "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId);";

            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var affectedRows = await dbConnection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
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
