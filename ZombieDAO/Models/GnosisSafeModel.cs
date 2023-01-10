namespace ZombieDAO.Models; 

[Table("gnosis_safes")]
public sealed class GnosisSafeModel {
    [Key, Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
    
    [ForeignKey("projects.id"), Column("project_id")]
    public required Guid ProjectID { get; init; }
    
    [Column("name")]
    public required string Name { get; init; }
    
    [Column("chain_id")]
    public required int ChainID { get; init; }
    
    [Column("address")]
    public required string Address { get; init; }
    
    public ProjectModel? Project { get; set; }
    public List<GnosisSafeTransactionModel> Transactions { get; set; } = new();
    public List<GnosisSafeTokenModel> Tokens { get; set; } = new();

    public static GnosisSafeModel Create(CreateGnosisSafeDTO dto, Guid projectID) {
        return new GnosisSafeModel {
            Name = dto.Name, ChainID = dto.ChainID, Address = dto.Address, ProjectID = projectID
        };
    }
}
