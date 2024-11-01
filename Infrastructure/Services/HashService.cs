using Application.Services;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class HashService : IHashService
{
    public string ComputeHash(string value)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(value);

        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] hashBytes = sha512.ComputeHash(textBytes);
            StringBuilder hash = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
            {
                hash.AppendFormat("{0:x2}", b);
            }

            return hash.ToString();
        }
    }
}
