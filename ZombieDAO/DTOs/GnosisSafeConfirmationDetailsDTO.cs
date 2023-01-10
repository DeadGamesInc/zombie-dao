namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class GnosisSafeConfirmationDetailsDTO {
    [JsonProperty("id")]
    public required Guid ID { get; init; }
    
    [JsonProperty("signature")]
    public required string Signature { get; init; }
    
    [JsonProperty("wallet")]
    public required string UserWallet { get; init; }

    public static GnosisSafeConfirmationDetailsDTO Create(GnosisSafeConfirmationModel model) {
        return new GnosisSafeConfirmationDetailsDTO {
            ID = model.ID, Signature = model.Signature, UserWallet = model.UserWallet
        };
    }
}
