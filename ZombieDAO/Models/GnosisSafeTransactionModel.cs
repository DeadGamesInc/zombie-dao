namespace ZombieDAO.Models; 

[Table("gnosis_safe_transactions")]
public sealed class GnosisSafeTransactionModel {
    [Key, Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
    
    [ForeignKey("gnosis_safes.id"), Column("safe")]
    public required Guid GnosisSafeID { get; init; }
    
    [ForeignKey("users.wallet"), Column("user")]
    public required string UserWallet { get; init; }
    
    [Column("to")]
    public required string To { get; init; }
    
    [Column("data")]
    public required string Data { get; init; }
    
    [Column("operation")]
    public required int Operation { get; init; }
    
    [Column("safe_tx_gas")]
    public required string SafeTxGas { get; init; }
    
    [Column("base_gas")]
    public required string BaseGas { get; init; }
    
    [Column("gas_price")]
    public required string GasPrice { get; init; }
    
    [Column("gas_token")]
    public required string GasToken { get; init; }
    
    [Column("refund_receiver")]
    public required string RefundReceiver { get; init; }
    
    [Column("value")]
    public required string Value { get; init; }
    
    [Column("nonce")]
    public required int Nonce { get; init; }
    
    [Column("executed")]
    public bool Executed { get; set; } = false;
    
    public GnosisSafeModel? GnosisSafe { get; set; }
    public UserModel? User { get; set; }
    public List<GnosisSafeConfirmationModel> Confirmations { get; set; } = new();

    public static GnosisSafeTransactionModel Create(CreateGnosisSafeTransactionDTO dto, Guid safeID, string wallet) {
        return new GnosisSafeTransactionModel {
            GnosisSafeID = safeID, UserWallet = wallet, To = dto.To, Operation = dto.Operation, SafeTxGas = dto.SafeTxGas,
            BaseGas = dto.BaseGas, Data = dto.Data, Nonce = dto.Nonce, Value = dto.Value, RefundReceiver = dto.RefundReceiver,
            GasToken = dto.GasToken, GasPrice = dto.GasPrice
        };
    }
}
