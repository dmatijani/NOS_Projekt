using Application.Models;

namespace Application.Services;

public interface IAesKeyGenerator
{
    public AesKeyIv GenerateKeyIv();
}
