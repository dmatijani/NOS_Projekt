using System.Security.Cryptography;

namespace WASM.Services;

public class DigitalSignatureService : IDigitalSignatureService
{
    public string Sign(string value, string privateKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            byte[] valueBytes = System.Text.Encoding.UTF8.GetBytes(value);
            byte[] signatureBytes = rsa.SignData(valueBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }
    }

    public bool Verify(string value, string signature, string publicKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            byte[] valueBytes = System.Text.Encoding.UTF8.GetBytes(value);
            byte[] signatureBytes = Convert.FromBase64String(signature);

            return rsa.VerifyData(valueBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
