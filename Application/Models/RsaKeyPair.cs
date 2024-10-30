namespace Application.Models;

public record RsaKeyPair
{
    string PublicKey { get; set; }

    string PrivateKey { get; set; }
}
