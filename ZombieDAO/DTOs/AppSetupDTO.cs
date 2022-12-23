namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class AppSetupDTO {
    [JsonProperty("supported_blockchains")]
    public required BlockchainNode[] SupportedBlockchains { get; init; }
}
