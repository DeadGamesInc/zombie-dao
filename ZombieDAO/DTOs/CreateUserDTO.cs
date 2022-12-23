namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CreateUserDTO {
    [JsonProperty("wallet")]
    public required string Wallet { get; init; }
    
    [JsonProperty("display_name"), StringLength(30, MinimumLength = 5)]
    public required string DisplayName { get; init; }
    
    [JsonProperty("signature")]
    public required string Signature { get; init; }
}
