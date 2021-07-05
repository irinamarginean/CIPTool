using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public sealed class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
