using System.Security.Cryptography;
using System.Text;

namespace WASM.Services;

public class AesService : IAesService
{
    public string Decrypt(string encryptedValue, string key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

    public string Encrypt(string value, string key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
