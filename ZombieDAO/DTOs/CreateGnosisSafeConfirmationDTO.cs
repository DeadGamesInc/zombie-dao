namespace ZombieDAO.DTOs; 

[Serializable]
public sealed class CreateGnosisSafeConfirmationDTO {
    [JsonProperty("signature")]
    public required string Signature { get; init; }
}
