using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Ideas
{
    public interface IIdeaService
    {
        Task<ICollection<IdeaEntity>> GetAllIdeas();
        Task<IdeaEntity> GetIdeaById(string id);
        Task<string> GetIdeaPredictedNumber(string group, string countryCode = "RBRO");
        Task<ICollection<IdeaEntity>> GetIdeasByAssociate(string username);
        Task AddIdea(IdeaEntity idea);
        Task UpdateIdea(IdeaEntity ideaToUpdate);
        Task DeleteIdea(IdeaEntity ideaToDelete);
        Task<List<Category>> GetIdeaCategories(IdeaEntity idea, List<string> categories);
        Task<ICollection<IdeaEntity>> GetWaitingForApprovalIdeasByReviewer(Associate reviewer);
        Task<ICollection<LeaderResponse>> GetLeaderResponsesByAssociate(Associate reviewer);
        Task SaveLeaderResponse(IdeaEntity idea, LeaderResponseDto leaderResponseDto);
        Task UpdateReviewer(IdeaEntity idea, string reviewerId);
        Task<Attachment> GetFileById(IdeaEntity idea, string fileId);
        Task<Attachment> GetFileByFilename(IdeaEntity idea, string filename);
        Task AddAttachment(ICollection<Attachment> attachmentsToAdd);
    }
}
