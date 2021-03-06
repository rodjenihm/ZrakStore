﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Data.Utilities;

namespace ZrakStore.Data.Repositories
{
    public class DapperUserRepository : IAsyncUserRepository
    {
        private readonly DapperConnectionString connectionString;

        public DapperUserRepository(DapperConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task AddAsync(User user)
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var affectedRows = await dbConnection.ExecuteAsync("spUsers_Add @Id, @Username, @PasswordHash", user);
            }
        }

        public async Task<int> CountAsync()
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var count = await dbConnection.ExecuteScalarAsync<int>("spUsers_Count");
                return count;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var users = await dbConnection.QueryAsync<User>("spUsers_GetAll");
                return users;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var user = (await dbConnection.QueryAsync<User>("spUsers_GetById @Id", new { Id = id })).FirstOrDefault();
                return user;
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            using (var dbConnection = new SqlConnection(connectionString.Value))
            {
                var user = (await dbConnection.QueryAsync<User>("spUsers_GetByUsername @Username", new { UserName = username })).FirstOrDefault();
                return user;
            }
        }

        public Task RemoveAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
