namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class AddProjectMemberDTO {
    [JsonProperty("wallet")]
    public required string Wallet { get; init; }
    
    [JsonProperty("level")]
    public required ProjectMemberLevel Level { get; init; }
}
