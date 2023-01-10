namespace ZombieDAO.Models; 

[Table("gnosis_safe_tokens")]
public sealed class GnosisSafeTokenModel {
    [Key, Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
    
    [ForeignKey("gnosis_safes.id"), Column("safe_id")]
    public required Guid SafeID { get; init; }
    
    [Column("symbol")]
    public required string Symbol { get; init; }
    
    [Column("address")]
    public required string Address { get; init; }
    
    public GnosisSafeModel? Safe { get; set; }
}
