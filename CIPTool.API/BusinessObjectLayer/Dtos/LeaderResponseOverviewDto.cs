using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class LeaderResponseOverviewDto
    {
        public ICollection<IdeaOverviewDto> WaitingForApprovalIdeasOverview { get; set; }
        public ICollection<IdeaOverviewDto> LeaderResponses { get; set; }
    }
}
