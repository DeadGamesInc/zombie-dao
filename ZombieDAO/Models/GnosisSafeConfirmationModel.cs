namespace ZombieDAO.Models; 

[Table("safe_confirmations")]
public sealed class GnosisSafeConfirmationModel {
    [Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
    
    [Column("signature")]
    public required string Signature { get; init; }
    
    [ForeignKey("gnosis_safe_transactions.id"), Column("transaction")]
    public required Guid TransactionID { get; init; }
    
    [ForeignKey("users.wallet"), Column("user")]
    public required string UserWallet { get; init; }
    
    public GnosisSafeTransactionModel? Transaction { get; set; }
    public UserModel? User { get; set; }
}
