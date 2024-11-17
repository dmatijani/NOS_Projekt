using WASM.Models;

namespace WASM.Services;

public interface IRsaKeyGenerator
{
    public RsaKeyPair GenerateKeyPair();
}
