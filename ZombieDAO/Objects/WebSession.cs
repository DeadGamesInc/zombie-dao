namespace ZombieDAO.Objects; 

public sealed class WebSession {
    public Guid ID { get; } = Guid.NewGuid();
    public DateTime LastAction { get; set; } = DateTime.UtcNow;
    public required UserDetailsDTO User { get; init; }
}
