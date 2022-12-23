namespace ZombieDAO.Models; 

[Table("project_members")]
public sealed class ProjectMemberModel {
    [ForeignKey("projects.id"), Column("project_id")]
    public required Guid ProjectID { get; init; }
    
    [ForeignKey("users.wallet"), Column("user_wallet")]
    public required string UserWallet { get; init; }
    
    [Column("level")]
    public required ProjectMemberLevel Level { get; set; }

    public ProjectModel? Project;
    public UserModel? User;
}
