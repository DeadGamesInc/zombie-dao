namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class ProjectDetailsDTO {
    [JsonProperty("id")]
    public required Guid ID { get; init; }
    
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("website")]
    public required string Website { get; init; }
    
    [JsonProperty("email_address")]
    public required string EmailAddress { get; init; }
    
    [JsonProperty("is_member")]
    public required bool IsMember { get; init; }
    
    [JsonProperty("member_level")]
    public required ProjectMemberLevel? Level { get; init; }
    
    [JsonProperty("members")]
    public required ProjectMemberDTO[] Members { get; init; }

    public static ProjectDetailsDTO Create(ProjectModel model, bool isMember = false, ProjectMemberLevel? level = null) {
        var members = Array.Empty<ProjectMemberDTO>();
        
        if (model.Members.Any()) {
            members = model.Members.Select(member => new ProjectMemberDTO
                { DisplayName = member.User?.DisplayName ?? string.Empty, Level = member.Level }).ToArray();
        }
        
        return new ProjectDetailsDTO {
            ID = model.ID, Name = model.Name, Website = model.Website, EmailAddress = model.EmailAddress,
            IsMember = isMember, Level = level, Members = members
        };
    }
}
