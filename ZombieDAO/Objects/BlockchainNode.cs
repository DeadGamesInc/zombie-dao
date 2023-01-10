namespace ZombieDAO.Objects; 

[Serializable]
public sealed class BlockchainNode {
    [JsonProperty("chain_id")]
    public required int ChainID { get; init; }
    
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("url")]
    public required string URL { get; init; }
    
    [JsonProperty("explorer_tx_prefix")]
    public required string ExplorerTxPrefix { get; init; }
    
    [JsonProperty("base_token")]
    public required string BaseToken { get; init; }
}
