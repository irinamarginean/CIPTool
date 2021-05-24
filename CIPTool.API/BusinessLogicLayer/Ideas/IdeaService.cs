using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Ideas
{
    public class IdeaService : IIdeaService
    {
        private readonly IdeaRepository ideaRepository;
        private readonly BaseRepository<Category> categoryRepository;
        private readonly BaseRepository<LeaderResponse> leaderResponseRepository;
        private readonly BaseRepository<Attachment> attachmentRepository;

        public IdeaService(
            IdeaRepository ideaRepository, 
            BaseRepository<Category> categoryRepository, 
            BaseRepository<LeaderResponse> leaderResponseRepository,
            BaseRepository<Attachment> attachmentRepository)
        {
            this.ideaRepository = ideaRepository;
            this.categoryRepository = categoryRepository;
            this.leaderResponseRepository = leaderResponseRepository;
            this.attachmentRepository = attachmentRepository;
        }

        public async Task<ICollection<IdeaEntity>> GetAllIdeas()
        {
            return await ideaRepository.GetAll();
        }

        public async Task<IdeaEntity> GetIdeaById(string id)
        {
            var ideas = await ideaRepository.GetAll();

            return ideas.FirstOrDefault(x => x.Id.ToString().Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<ICollection<IdeaEntity>> GetIdeasByAssociate(string username)
        {
            var ideas = await ideaRepository.GetAll();

            return ideas.Where(x => x.Associate.UserName == username).ToList();
        }

        public async Task AddIdea(IdeaEntity idea)
        {
            await ideaRepository.Insert(idea);
        }

        public async Task UpdateIdea(IdeaEntity ideaToUpdate)
        {
            await ideaRepository.Update(ideaToUpdate);
        }

        public async Task DeleteIdea(IdeaEntity ideaToDelete)
        {
            await ideaRepository.Delete(ideaToDelete);
        }

        public async Task<List<Category>> GetIdeaCategories(IdeaEntity idea, List<string> categories)
        {
            var allCategories = await categoryRepository.GetAll();
            var existingCategories = allCategories
                .Where(x => categories
                    .Any(c => c.ToUpperInvariant() == x.Text.ToUpperInvariant()))
                .ToList();
            List<string> stringCategoriesToAdd = categories.Except(existingCategories.Select(x => x.Text)).ToList();

            foreach (var categoryToAdd in stringCategoriesToAdd)
            {
                var category = new Category { Text = categoryToAdd };
                existingCategories.Add(category);
            }

            return existingCategories;
        }

        public async Task<string> GetIdeaPredictedNumber(string group, string countryCode = "RBRO")
        {
            var allIdeas = await ideaRepository.GetAll();
            var ideaCountByGroup = allIdeas.Count(x => x.Associate.Group == group);

            return $"{DateTime.Today.Year}_{countryCode}/{group}/{++ideaCountByGroup}";
        }

        public async Task<ICollection<IdeaEntity>> GetWaitingForApprovalIdeasByReviewer(Associate reviewer)
        {
            var allIdeas = await ideaRepository.GetAll();

            return allIdeas.Where(x => x.ReviewerId == reviewer.Id).ToList();
        }

        public async Task<ICollection<LeaderResponse>> GetLeaderResponsesByAssociate(Associate reviewer)
        {
            var allLeaderReponses = await leaderResponseRepository.GetAll();

            return allLeaderReponses.Where(x => x.ReviewerId == reviewer.Id).ToList();
        }

        public async Task SaveLeaderResponse(IdeaEntity idea, LeaderResponseDto leaderResponseDto)
        {
            var leaderResponse = new LeaderResponse
            {
                Id = Guid.NewGuid(),
                Idea = idea,
                IdeaId = Guid.Parse(leaderResponseDto.IdeaId),
                LeaderResponseDate = DateTime.Now,
                ReviewerId = leaderResponseDto.ReviewerId,
                Reason = leaderResponseDto.Reason,
                Response = leaderResponseDto.ResponseStatus
            };

            await ideaRepository.AddLeaderResponse(idea, leaderResponse);
        }

        public async Task<Attachment> GetFileById(IdeaEntity idea, string fileId)
        {
            var fileGuid = Guid.Parse(fileId);
            var attachment = idea.Attachments.FirstOrDefault(x => x.Id == fileGuid);

            return attachment;
        }

        public async Task<Attachment> GetFileByFilename(IdeaEntity idea, string filename)
        {
            var attachment = idea.Attachments.FirstOrDefault(x => x.FileName == filename);

            return attachment;
        }

        public async Task AddAttachment(ICollection<Attachment> attachmentsToAdd)
        {
            foreach (var attachment in attachmentsToAdd)
            {
                await attachmentRepository.Insert(attachment);
            }
        }
    }
}
