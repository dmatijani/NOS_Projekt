using Application.Services;
using Infrastructure.Services;

namespace Tests;

public class Sha512Tests
{
    [Fact]
    public void HashTest()
    {
        string text = "Ovo je nehashiran tekst!";
        IHashService hashService = new HashService();
        string hash = hashService.ComputeHash(text);
        string expected = "0c0db807dc76d1e761885295f84cb82a96afa69c1d3880b176114ef5e3e6f14850999d7fbb15b83f072c654240767bb0026713e5f39976e8a5f6eae2e4fa1635";
        Assert.Equal(expected, hash);
    }

    [Fact]
    public void HashesSameTest()
    {
        string text = "Ovo je nehashiran tekst!";
        IHashService hashService = new HashService();
        string hash1 = hashService.ComputeHash(text);
        string hash2 = hashService.ComputeHash(text);
        Assert.Equal(hash1, hash2);
    }
}
