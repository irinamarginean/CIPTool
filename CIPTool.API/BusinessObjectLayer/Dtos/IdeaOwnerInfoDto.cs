namespace BusinessObjectLayer.Dtos
{
    public class IdeaOwnerInfoDto
    {
        public string Id { get; set; }
        public string AssociateName { get; set; }
        public bool IsLeader { get; set; }
        public string GroupLeaderName { get; set; }
        public string Group { get; set; }
        public string DepartmentLeaderName { get; set; }
        public string Department { get; set; }
        public string PredictedIdeaNumber { get; set; }
    }
}
