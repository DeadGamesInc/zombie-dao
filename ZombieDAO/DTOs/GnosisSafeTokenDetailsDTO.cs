namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class GnosisSafeTokenDetailsDTO {
    [JsonProperty("id")]
    public required Guid ID { get; init; }
    
    [JsonProperty("symbol")]
    public required string Symbol { get; init; }
    
    [JsonProperty("address")]
    public required string Address { get; init; }

    public static GnosisSafeTokenDetailsDTO Create(GnosisSafeTokenModel model) {
        return new GnosisSafeTokenDetailsDTO {
            ID = model.ID, Symbol = model.Symbol, Address = model.Address
        };
    }
}
