using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories.Abstract
{
    public interface IUserRepository : IRepository<Associate>
    {
        Task<Associate> GetAssociate(string username);
    }
}
