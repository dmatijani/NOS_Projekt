using Application.Services;
using Infrastructure.Services;
using System.Security.Cryptography;

namespace Tests;

public class AesTests
{
    [Fact]
    public void AesKeyTest()
    {
        IAesKeyGenerator keyGenerator = new AesKeyGenerator();
        string key = keyGenerator.GenerateKey();
    }

    [Fact]
    public void AesEncryptionTest()
    {
        IAesKeyGenerator keyGenerator = new AesKeyGenerator();
        string key = keyGenerator.GenerateKey();

        string text = "Bok, ovo je neki tekst!";
        IAesService aesService = new AesService();

        string encryptedText = aesService.Encrypt(text, key);
        string decryptedText = aesService.Decrypt(encryptedText, key);

        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public void AesWrongKeyTest()
    {
        IAesKeyGenerator keyGenerator = new AesKeyGenerator();
        string key1 = keyGenerator.GenerateKey();
        string key2 = keyGenerator.GenerateKey();

        string text = "Bok, ovo je neki tekst!";
        IAesService aesService = new AesService();

        string encryptedText = aesService.Encrypt(text, key1);

        Assert.Throws<CryptographicException>(() => aesService.Decrypt(encryptedText, key2));
    }

    [Fact]
    public void AesLongTextEncryptionTest()
    {
        IAesKeyGenerator keyGenerator = new AesKeyGenerator();
        string key = keyGenerator.GenerateKey();

        string text = "Ovo je neki jako dugački tekst koji se mora kriptirati. Ovaj tekst će se valjda dobro kriptirati i dekriptirati, jako je dugačak. Ovo je super dugačak tekst...";
        IAesService aesService = new AesService();

        string encryptedText = aesService.Encrypt(text, key);
        string decryptedText = aesService.Decrypt(encryptedText, key);

        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public void AesKeysDifferentTest()
    {
        IAesKeyGenerator keyGenerator = new AesKeyGenerator();
        string key1 = keyGenerator.GenerateKey();
        string key2 = keyGenerator.GenerateKey();

        Assert.NotEqual(key1, key2);
    }
}
