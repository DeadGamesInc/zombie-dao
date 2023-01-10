namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class GnosisSafeTransactionDetailsDTO {
    [JsonProperty("id")]
    public required Guid ID { get; init; }
    
    [JsonProperty("creator")]
    public required string Creator { get; init; }
    
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
    
    [JsonProperty("executed")]
    public required bool Executed { get; init; }
    
    [JsonProperty("confirmations")]
    public required GnosisSafeConfirmationDetailsDTO[] Confirmations { get; init; }

    [JsonProperty("has_signed")]
    public required bool HasSigned { get; init; }
    
    public static GnosisSafeTransactionDetailsDTO Create(GnosisSafeTransactionModel model, string wallet = "") {
        var confirmations = model.Confirmations.Select(GnosisSafeConfirmationDetailsDTO.Create).ToArray();
        var signed = confirmations.Any(a => a.UserWallet == wallet);
        
        return new GnosisSafeTransactionDetailsDTO {
            ID = model.ID, Creator = model.UserWallet, Data = model.Data, Executed = model.Executed, Nonce = model.Nonce,
            Operation = model.Operation, To = model.To, Value = model.Value, BaseGas = model.BaseGas,
            GasToken = model.GasToken, SafeTxGas = model.SafeTxGas, RefundReceiver = model.RefundReceiver, 
            GasPrice = model.GasPrice, Confirmations = confirmations, HasSigned = signed
        };
    }
}
