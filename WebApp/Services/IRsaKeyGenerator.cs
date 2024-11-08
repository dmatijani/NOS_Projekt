using WebApp.Models;

namespace WebApp.Services;

public interface IRsaKeyGenerator
{
    public RsaKeyPair GenerateKeyPair();
}
