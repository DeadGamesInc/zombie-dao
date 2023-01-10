namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CreateGnosisSafeTokenDTO {
    [JsonProperty("symbol")]
    public required string Symbol { get; init; }
    
    [JsonProperty("address")]
    public required string Address { get; init; }
}
