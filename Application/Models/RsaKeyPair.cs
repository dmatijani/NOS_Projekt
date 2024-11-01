namespace Application.Models;

public record RsaKeyPair
{
    public string PublicKey { get; set; }

    public string PrivateKey { get; set; }
}
