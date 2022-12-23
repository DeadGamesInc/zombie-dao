namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class ProjectMemberDTO {
    [JsonProperty("display_name")]
    public required string DisplayName { get; init; }
    
    [JsonProperty("level")]
    public required ProjectMemberLevel Level { get; init; }
}
