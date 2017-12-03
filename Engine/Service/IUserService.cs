using System.Threading.Tasks;
using Engine.Model;

namespace Engine.Service
{
    public interface IUserService
    {
        Task<User> GetUserById(long id);
    }
}