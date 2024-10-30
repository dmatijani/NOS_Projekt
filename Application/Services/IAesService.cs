using Application.Models;

namespace Application.Services;

public interface IAesService
{
    public string Encrypt(string value, AesKeyIv keyIv);

    public string Decrypt(string encryptedValue, AesKeyIv keyIv);
}
