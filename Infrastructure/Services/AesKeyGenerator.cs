using Application.Services;
using System.Security.Cryptography;

namespace Infrastructure.Services;

public class AesKeyGenerator : IAesKeyGenerator
{
    public string GenerateKey()
    {
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }
    }
}
