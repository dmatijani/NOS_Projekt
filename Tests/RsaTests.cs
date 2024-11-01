using Application.Models;
using Application.Services;
using Infrastructure.Services;
using System.Security.Cryptography;

namespace Tests;

public class RsaTests
{
    [Fact]
    public void RsaKeyTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();
        string publicKey = keys.PublicKey;
        string privateKey = keys.PrivateKey;
    }

    [Fact]
    public void RsaEncryptionTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        string text = "Bok, ovo je neki tekst!";
        IRsaService rsaService = new RsaService();

        string encryptedText = rsaService.Encrypt(text, keys.PublicKey);
        string decryptedText = rsaService.Decrypt(encryptedText, keys.PrivateKey);

        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public void RsaWrongKeyPairTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys1 = keyGenerator.GenerateKeyPair();
        RsaKeyPair keys2 = keyGenerator.GenerateKeyPair();

        string text = "Bok, ovo je neki tekst!";
        IRsaService rsaService = new RsaService();

        string encryptedText = rsaService.Encrypt(text, keys1.PublicKey);

        Assert.Throws<CryptographicException>(() => rsaService.Decrypt(encryptedText, keys2.PrivateKey));
    }

    [Fact]
    public void RsaLongTextEncryptionTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys = keyGenerator.GenerateKeyPair();

        string text = "Ovo je neki jako dugački tekst koji se mora kriptirati. Ovaj tekst će se valjda dobro kriptirati i dekriptirati, jako je dugačak. Ovo je super dugačak tekst...";
        IRsaService rsaService = new RsaService();

        string encryptedText = rsaService.Encrypt(text, keys.PublicKey);
        string decryptedText = rsaService.Decrypt(encryptedText, keys.PrivateKey);

        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public void RsaKeysDifferentTest()
    {
        IRsaKeyGenerator keyGenerator = new RsaKeyGenerator();
        RsaKeyPair keys1 = keyGenerator.GenerateKeyPair();
        RsaKeyPair keys2 = keyGenerator.GenerateKeyPair();

        Assert.NotEqual(keys1.PrivateKey, keys2.PrivateKey);
        Assert.NotEqual(keys1.PublicKey, keys2.PublicKey);
    }
}