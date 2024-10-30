using Application.Models;

namespace Application.Services;

public interface IRsaKeyGenerator
{
    public RsaKeyPair GenerateKeyPair();
}
