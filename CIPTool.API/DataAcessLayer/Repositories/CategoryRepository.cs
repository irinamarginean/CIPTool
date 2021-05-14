using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
