using System.Security.Cryptography;

namespace WASM.Services;

public class RsaService : IRsaService
{
    public string Decrypt(string encryptedValue, string key)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(key), out _);
            byte[] decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedValue), RSAEncryptionPadding.Pkcs1);

            return System.Text.Encoding.UTF8.GetString(decryptedData);
        }
    }

    public string Encrypt(string value, string key)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportRSAPublicKey(Convert.FromBase64String(key), out _);
            byte[] encryptedData = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(value), RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(encryptedData);
        }
    }
}
