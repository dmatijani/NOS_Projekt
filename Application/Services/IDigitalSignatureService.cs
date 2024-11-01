namespace Application.Services;

public interface IDigitalSignatureService
{
    public string Sign(string value, string privateKey);

    public bool Verify(string value, string signature, string publicKey);
}
