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

    [JsonProperty("tokens")]
    public required GnosisSafeTokenDetailsDTO[]? Tokens { get; init; }
    
    [JsonProperty("transactions")]
    public required GnosisSafeTransactionDetailsDTO[]? Transactions { get; init; }

    public static GnosisSafeDetailsDTO Create(GnosisSafeModel model, string wallet = "") {
        var tokens = model.Tokens.Select(GnosisSafeTokenDetailsDTO.Create).ToArray();
        var transactions = model.Transactions.Select(a => GnosisSafeTransactionDetailsDTO.Create(a, wallet)).ToArray();
        
        return new GnosisSafeDetailsDTO {
            ID = model.ID, Name = model.Name, ChainID = model.ChainID, Address = model.Address, Tokens = tokens,
            Transactions = transactions
        };
    }
}
