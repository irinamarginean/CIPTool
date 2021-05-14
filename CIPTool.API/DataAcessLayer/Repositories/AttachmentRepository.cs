using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class AttachmentRepository : BaseRepository<Attachment>
    {
        public AttachmentRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
