namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class GnosisSafeDetailsDTO {
    [JsonProperty("id")]
    public required Guid ID { get; init; }
    
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("chain_id")]
    public required int ChainID { get; init; }
    
    [JsonProperty("address")]
    public required string Address { get; init; }

    public static GnosisSafeDetailsDTO Create(GnosisSafeModel model) {
        return new GnosisSafeDetailsDTO {
            ID = model.ID, Name = model.Name, ChainID = model.ChainID, Address = model.Address
        };
    }
}
