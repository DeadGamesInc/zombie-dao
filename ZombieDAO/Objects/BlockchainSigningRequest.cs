namespace ZombieDAO.Objects; 

public sealed class BlockchainSigningRequest {
    public required string Wallet { get; init; }
    public required string Challenge { get; init; }
    public DateTime Requested { get; } = DateTime.UtcNow;
}
