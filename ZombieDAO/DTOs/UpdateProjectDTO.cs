namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class UpdateProjectDTO {
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("website")]
    public required string Website { get; init; }
    
    [JsonProperty("email_address")]
    public required string EmailAddress { get; init; }
}
