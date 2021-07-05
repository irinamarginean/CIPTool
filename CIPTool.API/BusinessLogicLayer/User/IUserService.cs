using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace BusinessLogicLayer.User
{
    public interface IUserService
    {
        Task<Associate> GetAssociate(string username);
    }
}
