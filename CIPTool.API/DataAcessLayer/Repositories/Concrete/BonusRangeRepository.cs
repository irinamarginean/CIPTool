using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusRangeRepository : BaseRepository<BonusRangeEntity>, IBonusRangeRepository
    {
        public BonusRangeRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
