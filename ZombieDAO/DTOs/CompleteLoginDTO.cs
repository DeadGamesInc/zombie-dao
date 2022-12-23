namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CompleteLoginDTO {
    [JsonProperty("wallet")]
    public required string Wallet { get; init; }
    
    [JsonProperty("signature")]
    public required string Signature { get; init; }
}
