using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace TestConsoleApp;

public class RsaEncryption
{
	private static RSACryptoServiceProvider _csp;
	private RSAParameters _privateKey;
	private RSAParameters _publicKey;

    public RsaEncryption()
    {
		_csp = new RSACryptoServiceProvider(2048);
		_privateKey = _csp.ExportParameters(true);
		_publicKey = _csp.ExportParameters(false);
    }

	public string GetPublicKey()
	{
		var sw = new StringWriter();
		var xs = new XmlSerializer(typeof(RSAParameters));
		xs.Serialize(sw, _publicKey);
		return sw.ToString();
	}

	public string GetPrivateKey()
	{
		var sw = new StringWriter();
		var xs = new XmlSerializer(typeof(RSAParameters));
		xs.Serialize(sw, _privateKey);
		return sw.ToString();
	}

	public string Encrypt(string plainText)
	{
		_csp = new RSACryptoServiceProvider();
		_csp.ImportParameters(_publicKey);
		var data = Encoding.Unicode.GetBytes(plainText);
		var cypher = _csp.Encrypt(data, false);
		return Convert.ToBase64String(cypher);
	}

	public string Decrypt(string cypherText)
	{
		var dataBytes = Convert.FromBase64String(cypherText);
		_csp.ImportParameters(_privateKey);
		var plainText = _csp.Decrypt(dataBytes, false);
		return Encoding.Unicode.GetString(plainText);
	}
}

internal class Program
{
	static void Main(string[] args)
	{
		RsaEncryption rsa = new RsaEncryption();
		string cypher = string.Empty;

		Console.WriteLine($"Public key: {rsa.GetPublicKey()}\n");
		Console.WriteLine($"Private key: {rsa.GetPrivateKey()}\n");

		Console.WriteLine("Enter your text to encrypt: ");
		var text = Console.ReadLine();
		if (!string.IsNullOrEmpty(text))
		{
			cypher = rsa.Encrypt(text);
			Console.WriteLine($"Encrypted text: {cypher}\n");
		}

		Console.WriteLine("Press any key to decrypt text.\n");
		Console.ReadLine();
		var plainText = rsa.Decrypt(cypher);
		Console.WriteLine($"Decrypted text: {plainText}\n");

		Console.WriteLine("End of program.\n");
		Console.ReadLine();
	}
}
