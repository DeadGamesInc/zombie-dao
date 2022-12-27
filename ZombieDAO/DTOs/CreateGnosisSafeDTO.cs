namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CreateGnosisSafeDTO {
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("chain_id")]
    public required int ChainID { get; init; }
    
    [JsonProperty("address")]
    public required string Address { get; init; }
}
