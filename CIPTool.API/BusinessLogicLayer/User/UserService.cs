using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;
using System.Threading.Tasks;

namespace BusinessLogicLayer.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Associate> GetAssociate(string username)
        {
            return await userRepository.GetAssociate(username);
        }
    }
}
