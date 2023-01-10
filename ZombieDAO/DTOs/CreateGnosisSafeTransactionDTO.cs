namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CreateGnosisSafeTransactionDTO {
    [JsonProperty("to")]
    public required string To { get; init; }
    
    [JsonProperty("data")]
    public required string Data { get; init; }
    
    [JsonProperty("operation")]
    public required int Operation { get; init; }
    
    [JsonProperty("safe_tx_gas")]
    public required string SafeTxGas { get; init; }
    
    [JsonProperty("base_gas")]
    public required string BaseGas { get; init; }
    
    [JsonProperty("gas_price")]
    public required string GasPrice { get; init; }
    
    [JsonProperty("gas_token")]
    public required string GasToken { get; init; }
    
    [JsonProperty("refund_receiver")]
    public required string RefundReceiver { get; init; }
    
    [JsonProperty("value")]
    public required string Value { get; init; }
    
    [JsonProperty("nonce")]
    public required int Nonce { get; init; }
}
