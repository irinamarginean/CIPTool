using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories
{
    public sealed class UserRepository : BaseRepository<Associate>, IUserRepository
    {
        private readonly CIPToolContext dataContext;

        public UserRepository(CIPToolContext dataContext) : base(dataContext) 
        {
            this.dataContext = dataContext;
        }

        public async Task<Associate> GetAssociate(string username)
        {
            return await dataContext.Users.OfType<Associate>()
                .AsNoTracking()
                .Include(x => x.Leader)
                    .ThenInclude(x => x.Leader)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
