using AspNetCore.Email;
using BusinessLogicLayer.FinancialReports;
using BusinessLogicLayer.Ideas;
using BusinessLogicLayer.User;
using BusinessObjectLayer;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories;
using DataAcessLayer.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CIPTool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IIdeaService ideaService;
        private readonly IUserService userService;
        private readonly IFinancialReportService financialReportService;

        public IdeasController(
            IIdeaService ideaService,
            IFinancialReportService financialReportService,
            IEmailSender emailSender,
            IUserService userService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.ideaService = ideaService;
            this.financialReportService = financialReportService;
            this.userService = userService;
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllIdeas()
        {
            var ideas = await ideaService.GetAllIdeas();
            var ideaOverviewDtos = new List<IdeaOverviewDto>();

            foreach (var idea in ideas)
            {
                var ideaDto = new IdeaOverviewDto
                {
                    Id = idea.Id,
                    Title = idea.Title,
                    Status = idea.Status,
                    PlanDate = idea.PlanDate,
                    ModifiedAt = idea.ModifiedAt,
                    ActualImplementationDate = idea.ActDate,
                    AssociateName = idea.Associate.DisplayName,
                    ReviewerName = idea.Associate.Leader?.DisplayName,
                    ResponsibleName = idea.Responsible?.DisplayName,
                    LeaderResponseAt = idea.LeaderResponses.OrderByDescending(x => x.LeaderResponseDate).Select(x => x.LeaderResponseDate).FirstOrDefault(),
                    FinancialReport = new FinancialReportDto
                    {
                        PlannedSavings = idea.FinancialReport.PlannedSavings,
                        PlannedExpenses = idea.FinancialReport.PlannedExpenses,
                        PlannedBalance = idea.FinancialReport.PlannedBalance,
                        ActualSavings = idea.FinancialReport.ActualSavings,
                        ActualExpenses = idea.FinancialReport.ActualExpenses,
                        ActualBalance = idea.FinancialReport.ActualBalance,
                        Bonus = idea.FinancialReport.Bonus.Bonus
                    },
                    Categories = idea.Categories.Select(x => x.Text).ToList(),
                };

                ideaOverviewDtos.Add(ideaDto);
            }

            return Ok(ideaOverviewDtos);
        }

        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetIdeasByAssociate(string username)
        {
            var ideas = await ideaService.GetIdeasByAssociate(username);
            var ideaOverviewDtos = new List<IdeaOverviewDto>();

            foreach (var idea in ideas)
            {
                ideaOverviewDtos.Add(new IdeaOverviewDto
                {
                    Id = idea.Id,
                    Title = idea.Title,
                    Status = idea.Status,
                    PlanDate = idea.PlanDate,
                    ModifiedAt = idea.ModifiedAt,
                    ActualImplementationDate = idea.ActDate,
                    LeaderResponseAt = idea.LeaderResponses.OrderByDescending(x => x.LeaderResponseDate).Select(x => x.LeaderResponseDate).FirstOrDefault(),
                    AssociateName = idea.Associate.DisplayName,
                    ReviewerName = idea.Associate.Leader?.DisplayName,
                    ResponsibleName = idea.Responsible?.DisplayName,
                    Categories = idea.Categories.Select(x => x.Text).ToList(),
                    FinancialReport = new FinancialReportDto
                    {
                        PlannedSavings = idea.FinancialReport.PlannedSavings,
                        PlannedExpenses = idea.FinancialReport.PlannedExpenses,
                        PlannedBalance = idea.FinancialReport.PlannedBalance,
                        ActualSavings = idea.FinancialReport.ActualSavings,
                        ActualExpenses = idea.FinancialReport.ActualExpenses,
                        ActualBalance = idea.FinancialReport.ActualBalance,
                        Bonus = idea.FinancialReport.Bonus.Bonus
                    }
                });
            }

            return Ok(ideaOverviewDtos);
        }

        [HttpGet("responses-overview/{username}")]
        public async Task<IActionResult> GetLeaderResponseOverviewByAssociate(string username)
        {
            var reviewer = await userService.GetAssociate(username);
            var ideas = await ideaService.GetWaitingForApprovalIdeasByReviewer(reviewer);
            var waitingForApprovalIdeasDto = new List<IdeaOverviewDto>();

            foreach (var idea in ideas)
            {
                waitingForApprovalIdeasDto.Add(new IdeaOverviewDto
                {
                    Id = idea.Id,
                    Title = idea.Title,
                    Status = idea.Status,
                    PlanDate = idea.PlanDate,
                    ModifiedAt = idea.ModifiedAt,
                    LeaderResponseAt = idea.LeaderResponses.OrderByDescending(x => x.LeaderResponseDate).Select(x => x.LeaderResponseDate).FirstOrDefault(),
                    AssociateName = idea.Associate.DisplayName,
                    ReviewerName = idea.Associate.Leader.DisplayName,
                    ActualImplementationDate = idea.ActDate,
                    ResponsibleName = idea.Responsible.DisplayName,
                    Categories = idea.Categories.Select(x => x.Text).ToList(),
                    FinancialReport = new FinancialReportDto
                    {
                        PlannedSavings = idea.FinancialReport.PlannedSavings,
                        PlannedExpenses = idea.FinancialReport.PlannedExpenses,
                        PlannedBalance = idea.FinancialReport.PlannedBalance,
                        ActualSavings = idea.FinancialReport.ActualSavings,
                        ActualExpenses = idea.FinancialReport.ActualExpenses,
                        ActualBalance = idea.FinancialReport.ActualBalance,
                        Bonus = idea.FinancialReport.Bonus.Bonus
                    }
                });
            }

            var leaderResponses = await ideaService.GetLeaderResponsesByAssociate(reviewer);
            var ideasReviewedByLeader = leaderResponses.Select(x => x.Idea);

            var alreadyreviewedIdeasDto = new List<IdeaOverviewDto>();

            foreach (var idea in ideasReviewedByLeader)
            {
                alreadyreviewedIdeasDto.Add(new IdeaOverviewDto
                {
                    Id = idea.Id,
                    Title = idea.Title,
                    Status = idea.Status,
                    PlanDate = idea.PlanDate,
                    ModifiedAt = idea.ModifiedAt,
                    LeaderResponseAt = idea.LeaderResponses.OrderByDescending(x => x.LeaderResponseDate).Select(x => x.LeaderResponseDate).FirstOrDefault(),
                    AssociateName = idea.Associate.DisplayName,
                    ReviewerName = idea.Associate.Leader.DisplayName,
                    Categories = idea.Categories.Select(x => x.Text).ToList(),
                    FinancialReport = new FinancialReportDto
                    {
                        PlannedSavings = idea.FinancialReport.PlannedSavings,
                        PlannedExpenses = idea.FinancialReport.PlannedExpenses,
                        PlannedBalance = idea.FinancialReport.PlannedBalance,
                        ActualSavings = idea.FinancialReport.ActualSavings,
                        ActualExpenses = idea.FinancialReport.ActualExpenses,
                        ActualBalance = idea.FinancialReport.ActualBalance,
                        Bonus = idea.FinancialReport.Bonus.Bonus
                    }
                });
            }

            return Ok(new LeaderResponseOverviewDto
            {
                WaitingForApprovalIdeasOverview = waitingForApprovalIdeasDto,
                LeaderResponses = alreadyreviewedIdeasDto
            });
        }

        [HttpGet("add/load")]
        public async Task<IActionResult> GetAddIdeaInfo()
        {
            var addIdeaInfoDto = await GenerateAddIdeaInfoDto();

            return Ok(addIdeaInfoDto);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetIdeaById(string id)
        {
            var idea = await ideaService.GetIdeaById(id);

            var ideaDetailsDto = new IdeaDetailsDto
            {
                Id = idea.Id,
                IdeaNumber = idea.IdeaNumber,
                Title = idea.Title,
                Description = idea.Description,
                RichTextDescription = idea.RichTextDescription,
                Target = idea.Target,
                Context = idea.Context,
                ReviewerName = idea.Reviewer.DisplayName,
                Status = idea.Status,
                PlanDate = idea.PlanDate,
                DoDate = idea.DoDate,
                CheckDate = idea.CheckDate,
                ActDate = idea.ActDate,
                ModifiedAt = idea.ModifiedAt,
                IsAssociateResponsible = idea.IsAssociateResponsible,
                LeaderResponseAt = idea.LeaderResponses.OrderByDescending(x => x.LeaderResponseDate).Select(x => x.LeaderResponseDate).FirstOrDefault(),
                Categories = idea.Categories.Select(x => x.Text).ToList(),
                IdeaOwnerDetails = await GenerateAddIdeaInfoDto(idea.Associate.UserName),
                FinancialReport = new FinancialReportDto
                {
                    PlannedSavings = idea.FinancialReport.PlannedSavings,
                    PlannedExpenses = idea.FinancialReport.PlannedExpenses,
                    PlannedBalance = idea.FinancialReport.PlannedBalance,
                    ActualSavings = idea.FinancialReport.ActualSavings,
                    ActualExpenses = idea.FinancialReport.ActualExpenses,
                    ActualBalance = idea.FinancialReport.ActualBalance,
                    Bonus = idea.FinancialReport.Bonus.Bonus
                },
                Attachments = idea.Attachments.Select(x => new AttachmentDetailsDto
                {
                    Id = x.Id.ToString(),
                    FileName = x.FileName,
                    UploadedAt = x.UploadedAt,
                }).ToList(),
                LeaderResponses = idea.LeaderResponses.Select(x => new LeaderResponseDetailsDto
                {
                    IdeaId = x.IdeaId.ToString(),
                    ReviewerName = x.Reviewer.DisplayName,
                    ResponseStatus = x.Response,
                    Reason = x.Reason,
                    Date = x.LeaderResponseDate
                }).ToList()
            };

            return Ok(ideaDetailsDto);
        }

        [HttpPost("add/{id}")]
        public async Task<IActionResult> AddIdea(string id, [FromBody] AddIdeaDto addIdeaDto)
        {
            var currentUser = await GetCurrentUser();
            var currentAssociate = await userService.GetAssociate(currentUser.UserName);
            var responsible = await userService.GetAssociate(addIdeaDto.ImplementationResponsible.UserName);
            var financialReport = new FinancialReportEntity
            {
                Id = Guid.NewGuid(),
                PlannedSavings = addIdeaDto.FinancialReport.PlannedSavings,
                PlannedExpenses = addIdeaDto.FinancialReport.PlannedExpenses,
                PlannedBalance = addIdeaDto.FinancialReport.PlannedSavings - addIdeaDto.FinancialReport.PlannedExpenses,
                ActualSavings = addIdeaDto.FinancialReport.ActualSavings,
                ActualExpenses = addIdeaDto.FinancialReport.ActualExpenses,
                ActualBalance = addIdeaDto.FinancialReport.ActualSavings - addIdeaDto.FinancialReport.ActualExpenses,
                UploadedAt = addIdeaDto.FinancialReport.UploadedAt,
                ModifiedAt = addIdeaDto.FinancialReport.ModifiedAt,
            };
            var attachments = new List<Attachment>();

            foreach (var attachmentDto in addIdeaDto.Attachments)
            {
                var folderName = Path.Combine("Resources", "Ideas", currentAssociate.UserName, id);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                attachments.Add(new Attachment
                { 
                    Id = Guid.NewGuid(),
                    FileName = attachmentDto.FileName,
                    Location = Path.Combine(pathToSave, attachmentDto.FileName),
                    UploadedAt = attachmentDto.UploadedAt,
                });
            }

            var idea = new IdeaEntity
            {
                Id = Guid.Parse(id),
                IdeaNumber = await ideaService.GetIdeaPredictedNumber(currentAssociate.Group),
                Title = addIdeaDto.Title,
                Description = addIdeaDto.Description,
                RichTextDescription = addIdeaDto.RichTextDescription,
                Target = addIdeaDto.Target,
                Context = addIdeaDto.Context,
                Status = ResponseStatus.WaitingForApprovalStatus,
                IsAssociateResponsible = addIdeaDto.IsAssociateResponsible,
                PlanDate = DateTime.Now,
                DoDate = addIdeaDto.DoDate,
                AssociateId = currentAssociate.Id,
                ReviewerId = currentAssociate.Leader.Id,
                ResponsibleId = responsible.Id,
                FinancialReportId = financialReport.Id,
                FinancialReport = financialReport,
                Attachments = attachments
            };

            idea.Categories = await ideaService.GetIdeaCategories(idea, addIdeaDto.Categories);
            var bonusTask = financialReportService.GenerateBonus(idea);
            var bonus = await bonusTask;
            idea.FinancialReport.Bonus = bonus;
            idea.FinancialReport.BonusId = bonus.Id;

            await ideaService.AddIdea(idea);

            await emailSender.SendEmailAsync(
                                    "fixed-term.Irina.Marginean@ro.bosch.com",
                                    "Idea submission in CIP Tool\n",
                                    "Thank you for your involvement in the Continuous Improvement Process.\n" +
                                    "Have a nice day, \nSupport ECC CIP Tool");

            return Ok();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditIdea(string id, [FromBody] UpdateIdeaDto updateIdeaDto)
        {
            var ideaToUpdate = await ideaService.GetIdeaById(id);

            if (ideaToUpdate == null) return NotFound("No such idea found.");

            ideaToUpdate.FinancialReport.PlannedSavings = updateIdeaDto.FinancialReport.PlannedSavings;
            ideaToUpdate.FinancialReport.PlannedExpenses = updateIdeaDto.FinancialReport.PlannedExpenses;
            ideaToUpdate.FinancialReport.PlannedBalance = updateIdeaDto.FinancialReport.PlannedSavings - updateIdeaDto.FinancialReport.PlannedExpenses;
            ideaToUpdate.FinancialReport.ActualSavings = updateIdeaDto.FinancialReport.ActualSavings;
            ideaToUpdate.FinancialReport.ActualExpenses = updateIdeaDto.FinancialReport.ActualExpenses;
            ideaToUpdate.FinancialReport.ActualBalance = updateIdeaDto.FinancialReport.ActualSavings - updateIdeaDto.FinancialReport.ActualExpenses;
            ideaToUpdate.FinancialReport.ModifiedAt = DateTime.Now;
            ideaToUpdate.ModifiedAt = DateTime.Now;
            ideaToUpdate.Title = updateIdeaDto.Title;
            ideaToUpdate.Description = updateIdeaDto.Description;
            ideaToUpdate.RichTextDescription = updateIdeaDto.RichTextDescription;
            ideaToUpdate.Context = updateIdeaDto.Context;
            ideaToUpdate.Target = updateIdeaDto.Target;
            ideaToUpdate.IsAssociateResponsible = updateIdeaDto.IsAssociateResponsible;
            ideaToUpdate.DoDate = updateIdeaDto.DoDate;
            ideaToUpdate.CheckDate = updateIdeaDto.CheckDate;
            ideaToUpdate.ActDate = updateIdeaDto.ActDate;
            ideaToUpdate.Categories = await ideaService.GetIdeaCategories(ideaToUpdate, updateIdeaDto.Categories);
            var bonus = await financialReportService.GenerateBonus(ideaToUpdate);
            ideaToUpdate.FinancialReport.Bonus.Bonus = bonus.Bonus;
            ideaToUpdate.FinancialReport.Bonus.BonusCorrectionFactorId = bonus.BonusCorrectionFactorId;
            ideaToUpdate.FinancialReport.Bonus.BonusRangeId = bonus.BonusRangeId;

            var attachments = ideaToUpdate.Attachments
                .Where(x => updateIdeaDto.Attachments
                    .Select(a => a.FileName)
                    .Contains(x.FileName)).ToList();
            var newAttachmentDtos = updateIdeaDto.Attachments
                .Where(x => !ideaToUpdate.Attachments
                    .Select(a => a.FileName)
                    .Contains(x.FileName)).ToList();

            var newAttachments = new List<Attachment>();

            foreach (var attachmentDto in newAttachmentDtos)
            {
                var folderName = Path.Combine("Resources", "Ideas", ideaToUpdate.Associate.UserName, id);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                newAttachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    FileName = attachmentDto.FileName,
                    Location = Path.Combine(pathToSave, attachmentDto.FileName),
                    UploadedAt = attachmentDto.UploadedAt,
                    Idea = ideaToUpdate
                });
            }

            ideaToUpdate.Attachments = attachments;

            await ideaService.UpdateIdea(ideaToUpdate);
            await ideaService.AddAttachment(newAttachments);

            return Ok();
        }

        [HttpPut("{ideaId}/confirm-implementation")]
        public async Task<IActionResult> ConfirmImplementation(string ideaId)
        {
            var idea = await ideaService.GetIdeaById(ideaId);

            if (idea == null) return NotFound("No such idea found in the tool.");

            if (idea.Status != ResponseStatus.ApprovedStatus) return BadRequest("The idea has not been approved yet.");

            idea.Status = ResponseStatus.ImplementedStatus;
            idea.ActDate = DateTime.Now;

            await ideaService.UpdateIdea(idea);

            return Ok();
        }

        [HttpPut("leader-response/{ideaId}")]
        public async Task<IActionResult> GiveLeaderResponse(string ideaId, [FromBody] LeaderResponseDto leaderResponseDto)
        {
            var idea = await ideaService.GetIdeaById(ideaId);

            if (idea == null) return NotFound("No such idea found in the tool.");

            await ideaService.SaveLeaderResponse(idea, leaderResponseDto);

            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateIdea(string id, [FromBody] AddIdeaDto ideaToUpdate)
        {
            var idea = await ideaService.GetIdeaById(id);

            if (idea == null) return NotFound("The idea was not found!");

            idea.Title = ideaToUpdate.Title;
            idea.Description = ideaToUpdate.Description;
            idea.Target = ideaToUpdate.Target;
            idea.IsAssociateResponsible = ideaToUpdate.IsAssociateResponsible;
            idea.ModifiedAt = DateTime.Now;

            await ideaService.UpdateIdea(idea);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteIdea(string id)
        {
            var idea = await ideaService.GetIdeaById(id);

            if (idea == null) return NotFound("The idea was not found!");

            await ideaService.DeleteIdea(idea);

            return Ok();
        }

        public async Task<Associate> GetCurrentUser()
        {
            Associate associate;

            var email = User.FindFirst(ClaimTypes.Email).Value;
            associate = userManager.Users.SingleOrDefault(r => r.Email == email) as Associate;

            if (associate == null)
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                associate = userManager.Users.SingleOrDefault(r => r.UserName == username) as Associate;
            }

            return associate;
        }

        [HttpPost("upload-files/{ideaNumber}")]
        public async Task<IActionResult> UploadFiles(string ideaNumber)
        {
            var files = Request.Form.Files;
            var user = await GetCurrentUser();
            var folderName = Path.Combine("Resources", "Ideas", user.UserName, ideaNumber);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            if (files.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName); 

                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
            }

            return Ok();
        }

        [HttpGet("download-file/byId")]
        public async Task<IActionResult> DownloadFile(string ideaId, string fileId)
        {
            var idea = await ideaService.GetIdeaById(ideaId);

            if (idea == null)
            {
                return NotFound("Idea not found!");
            }

            var file = await ideaService.GetFileById(idea, fileId);

            if (file == null)
            {
                return NotFound("File not found!");
            }

            var filePath = file.Location;
            var fileName = file.FileName;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpDelete("delete-file/byFilename")]
        public async Task<IActionResult> DeleteFile(string ideaId, string filename)
        {
            var user = await GetCurrentUser();
            var folderName = Path.Combine("Resources", "Ideas", user.UserName, ideaId);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(folderPath, filename);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

                return Ok();
            }

            return BadRequest("Could not delete the file!");
        }

        [HttpGet("users/all")]
        public async Task<IActionResult> GetAllAssociates()
        {
            var allUsers = userManager.Users.Select(x => x as Associate).ToList();
            var allUsersOverviewDtos = allUsers.Select(x => new UserOverviewDto
            {
                UserName = x.UserName,
                DisplayName = x.DisplayName
            }).ToList();

            return Ok(allUsersOverviewDtos);
        }

        private async Task<IdeaOwnerInfoDto> GenerateAddIdeaInfoDto(string username = "")
        {
            Associate associate;
            if (string.IsNullOrEmpty(username))
            {
                var currentLoggedUser = await GetCurrentUser();
                associate = await userService.GetAssociate(currentLoggedUser.UserName);
            }
            else
            {
                associate = await userService.GetAssociate(username);
            }

            var addIdeaInfoDto = new IdeaOwnerInfoDto
            {
                Id = Guid.NewGuid().ToString(),
                AssociateName = $"{associate.FirstName} {associate.LastName}",
                IsLeader = associate.IsLeader,
                Group = associate.Group,
                GroupLeaderName = $"{associate.Leader?.FirstName} {associate.Leader?.LastName}",
                Department = associate.Department,
                DepartmentLeaderName = $"{associate.Leader?.Leader?.FirstName} {associate.Leader?.Leader?.LastName}",
                PredictedIdeaNumber = await ideaService.GetIdeaPredictedNumber(associate.Group)
            };

            if (addIdeaInfoDto.IsLeader && associate.Leader == null)
            {
                addIdeaInfoDto.DepartmentLeaderName = $"{associate.FirstName} {associate.LastName}";
                addIdeaInfoDto.Department = associate.Department;
            }
            else if (addIdeaInfoDto.IsLeader && associate.Leader.Leader == null)
            {
                addIdeaInfoDto.GroupLeaderName = $"{associate.FirstName} {associate.LastName}";
                addIdeaInfoDto.Group = associate.Group;
                addIdeaInfoDto.DepartmentLeaderName = $"{associate.Leader.FirstName} {associate.Leader.LastName}";
                addIdeaInfoDto.Department = associate.Department;
            }

            return addIdeaInfoDto;
        }

        [HttpPut("{ideaId}/update-reviewer/{username}")]
        public async Task<IActionResult> UpdateReviwer(string ideaId, string username)
        {
            var idea = await ideaService.GetIdeaById(ideaId);
            var user = await userService.GetAssociate(username);

            if (idea == null) return NotFound("No such idea found in the tool.");
            if (user == null) return NotFound("No such user found in the tool.");

            idea.ReviewerId = user.Id;

            await ideaService.UpdateIdea(idea);

            return Ok();
        }
    }
}
