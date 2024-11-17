using WASM.Models;
using System.Security.Cryptography;

namespace WASM.Services;

public class RsaKeyGenerator : IRsaKeyGenerator
{
    public RsaKeyPair GenerateKeyPair()
    {
        using (var rsa = RSA.Create(2048))
        {
            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            return new RsaKeyPair
            {
                PublicKey = publicKey,
                PrivateKey = privateKey
            };
        }
    }
}
