namespace WASM.Services;

public interface IAesService
{
    public string Encrypt(string value, string key);

    public string Decrypt(string encryptedValue, string key);
}
