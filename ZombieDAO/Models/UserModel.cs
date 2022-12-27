namespace ZombieDAO.Models; 

[Table("users")]
public sealed class UserModel {
    [Key, Column("wallet")]
    public required string Wallet { get; init; }
    
    [Column("display_name")]
    public required string DisplayName { get; init; }

    public List<ProjectMemberModel> Projects { get; set; } = new();
    public List<GnosisSafeTransactionModel> GnosisSafeTransactions { get; set; } = new();
    public List<GnosisSafeConfirmationModel> GnosisSafeConfirmations { get; set; } = new();

    public static UserModel Create(CreateUserDTO dto) {
        return new UserModel {
            Wallet = dto.Wallet, DisplayName = dto.DisplayName
        };
    }
}
