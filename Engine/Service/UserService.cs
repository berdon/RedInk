using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Engine.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Service
{
    public class UserService : IUserService
    {
        private IServiceProvider _serviceProvider;

        public UserService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task<User> GetUserById(long id)
        {
            using (var scope = _serviceProvider.CreateScope()) {
                var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
                return await connection.QueryFirstOrDefaultAsync<User>("select * from users where id = @id", new { id });
            }
        }
    }
}