namespace ZombieDAO.Models; 

[Table("projects")]
public sealed class ProjectModel {
    [Key, Column("id")]
    public Guid ID { get; set; } = Guid.NewGuid();
    
    [Column("name")]
    public required string Name { get; set; }
    
    [Column("website")]
    public required string Website { get; set; }
    
    [Column("email_address")]
    public required string EmailAddress { get; set; }

    public List<ProjectMemberModel> Members { get; set; } = new();

    public static ProjectModel Create(CreateProjectDTO dto) {
        return new ProjectModel {
            Name = dto.Name, Website = dto.Website, EmailAddress = dto.EmailAddress
        };
    }

    public void Update(UpdateProjectDTO dto) {
        Name = dto.Name;
        Website = dto.Website;
        EmailAddress = dto.EmailAddress;
    }
}
