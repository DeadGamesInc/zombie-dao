namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class UserDetailsDTO {
    [JsonProperty("wallet")]
    public required string Wallet { get; init; }
    
    [JsonProperty("display_name")]
    public required string DisplayName { get; init; }
    
    [JsonProperty("projects")]
    public required ProjectDetailsDTO[] Projects { get; init; }

    public static UserDetailsDTO Create(UserModel model) {
        var projects = Array.Empty<ProjectDetailsDTO>();
        if (model.Projects.Any()) {
            projects = model.Projects.Select(project => ProjectDetailsDTO.Create(project.Project, true, project.Level)).ToArray();
        }
        
        return new UserDetailsDTO { Wallet = model.Wallet, DisplayName = model.DisplayName, Projects = projects };
    }
}
