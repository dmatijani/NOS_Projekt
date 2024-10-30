using Application.Models;
using Application.Services;

namespace Infrastructure.Services;

public class AesService : IAesService
{
    public string Decrypt(string encryptedValue, AesKeyIv keyIv)
    {
        throw new NotImplementedException();
    }

    public string Encrypt(string value, AesKeyIv keyIv)
    {
        throw new NotImplementedException();
    }
}
