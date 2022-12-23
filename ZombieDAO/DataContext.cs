namespace ZombieDAO; 

public sealed class DataContext : DbContext {
    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
    public DbSet<ProjectMemberModel> ProjectMembers => Set<ProjectMemberModel>();
    
    public DataContext() { }
    public DataContext(DbContextOptions options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder) {
        var connect = Environment.GetEnvironmentVariable("ZOMBIE_DAO_DBCONN") 
                      ?? throw new Exception("Must provide a PostgreSQL connection string");
        builder.UseNpgsql(connect);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ProjectMemberModel>(entity => {
            entity.HasKey(member => new { member.ProjectID, member.UserWallet });
            
            entity
                .HasOne(member => member.Project)
                .WithMany(project => project.Members)
                .HasForeignKey(member => member.ProjectID);

            entity
                .HasOne(member => member.User)
                .WithMany(user => user.Projects)
                .HasForeignKey(member => member.UserWallet);
        });

        modelBuilder.Entity<UserModel>(entity => {
            entity
                .HasMany(user => user.Projects)
                .WithOne(member => member.User)
                .HasForeignKey(member => member.UserWallet);
        });

        modelBuilder.Entity<ProjectModel>(entity => {
            entity
                .HasMany(project => project.Members)
                .WithOne(member => member.Project)
                .HasForeignKey(member => member.ProjectID);
        });
    }
}
