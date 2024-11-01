using Application.Models;
using Application.Services;
using Infrastructure.Services;

namespace Tests;

public class DigitalSignatureTests
{
    [Fact]
    public void DigitalSignTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        IDigitalSignatureService digitalSignatureService = new DigitalSignatureService();
        string text = "Ovo je tekst kojemu se treba provjeriti digitalni potpis!";
        string signature = digitalSignatureService.Sign(text, keys.PrivateKey);

        bool verified = digitalSignatureService.Verify(text, signature, keys.PublicKey);
        Assert.True(verified);
    }

    [Fact]
    public void TextAlteredTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        IDigitalSignatureService digitalSignatureService = new DigitalSignatureService();
        string text = "Ovo je tekst kojemu se treba provjeriti digitalni potpis!";
        string signature = digitalSignatureService.Sign(text, keys.PrivateKey);

        text = "Ovaj tekst je mijenjan!";

        bool verified = digitalSignatureService.Verify(text, signature, keys.PublicKey);
        Assert.False(verified);
    }

    [Fact]
    public void TextAlteredSlightlyTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        IDigitalSignatureService digitalSignatureService = new DigitalSignatureService();
        string text = "Ovo je tekst kojemu se treba provjeriti digitalni potpis!";
        string signature = digitalSignatureService.Sign(text, keys.PrivateKey);

        text = "ovo je tekst kojemu se treba provjeriti digitalni potpis!"; // O -> o

        bool verified = digitalSignatureService.Verify(text, signature, keys.PublicKey);
        Assert.False(verified);
    }

    [Fact]
    public void SignatureAlteredText()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        IDigitalSignatureService digitalSignatureService = new DigitalSignatureService();
        string text = "Ovo je tekst kojemu se treba provjeriti digitalni potpis!";
        string signature = digitalSignatureService.Sign(text, keys.PrivateKey);

        RsaKeyPair otherKeys = keyGenerator.GenerateKeyPair();
        signature = digitalSignatureService.Sign(text, otherKeys.PrivateKey);

        bool verified = digitalSignatureService.Verify(text, signature, keys.PublicKey);
        Assert.False(verified);
    }
}
