namespace ZombieDAO; 

public sealed class DataContext : DbContext {
    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
    public DbSet<ProjectMemberModel> ProjectMembers => Set<ProjectMemberModel>();
    public DbSet<GnosisSafeModel> GnosisSafes => Set<GnosisSafeModel>();
    public DbSet<GnosisSafeTransactionModel> GnosisSafeTransactions => Set<GnosisSafeTransactionModel>();
    public DbSet<GnosisSafeConfirmationModel> GnosisSafeConfirmations => Set<GnosisSafeConfirmationModel>();
    
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

            entity
                .HasMany(user => user.GnosisSafeTransactions)
                .WithOne(tx => tx.User)
                .HasForeignKey(tx => tx.UserWallet);

            entity
                .HasMany(user => user.GnosisSafeConfirmations)
                .WithOne(confirmation => confirmation.User)
                .HasForeignKey(confirmation => confirmation.UserWallet);
        });

        modelBuilder.Entity<ProjectModel>(entity => {
            entity
                .HasMany(project => project.Members)
                .WithOne(member => member.Project)
                .HasForeignKey(member => member.ProjectID);

            entity
                .HasMany(project => project.GnosisSafes)
                .WithOne(safe => safe.Project)
                .HasForeignKey(safe => safe.ProjectID);
        });

        modelBuilder.Entity<GnosisSafeModel>(entity => {
            entity
                .HasOne(safe => safe.Project)
                .WithMany(project => project.GnosisSafes)
                .HasForeignKey(safe => safe.ProjectID);

            entity
                .HasMany(safe => safe.Transactions)
                .WithOne(tx => tx.GnosisSafe)
                .HasForeignKey(tx => tx.GnosisSafeID);
        });

        modelBuilder.Entity<GnosisSafeTransactionModel>(entity => {
            entity
                .HasOne(tx => tx.User)
                .WithMany(user => user.GnosisSafeTransactions)
                .HasForeignKey(tx => tx.UserWallet);

            entity
                .HasOne(tx => tx.GnosisSafe)
                .WithMany(safe => safe.Transactions)
                .HasForeignKey(tx => tx.GnosisSafeID);

            entity
                .HasMany(tx => tx.Confirmations)
                .WithOne(confirmation => confirmation.Transaction)
                .HasForeignKey(confirmation => confirmation.TransactionID);
        });

        modelBuilder.Entity<GnosisSafeConfirmationModel>(entity => {
            entity
                .HasOne(confirmation => confirmation.Transaction)
                .WithMany(tx => tx.Confirmations)
                .HasForeignKey(tx => tx.TransactionID);

            entity
                .HasOne(confirmation => confirmation.User)
                .WithMany(user => user.GnosisSafeConfirmations)
                .HasForeignKey(confirmation => confirmation.UserWallet);
        });
    }
}
