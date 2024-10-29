using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace TestConsoleApp;

public class RsaEncryption
{
	private RSACryptoServiceProvider _csp;
	private RSAParameters _privateKey;
	private RSAParameters _publicKey;

	public RsaEncryption()
	{
		_csp = new RSACryptoServiceProvider(2048);
		_privateKey = _csp.ExportParameters(true);
		_publicKey = _csp.ExportParameters(false);
	}

	public void SetPrivateKey(string privateKeyXml)
	{
		var sr = new StringReader(privateKeyXml);
		var xs = new XmlSerializer(typeof(RSAParameters));
		_privateKey = (RSAParameters)xs.Deserialize(sr);
	}

	public void SetPublicKey(string publicKeyXml)
	{
		var sr = new StringReader(publicKeyXml);
		var xs = new XmlSerializer(typeof(RSAParameters));
		_publicKey = (RSAParameters)xs.Deserialize(sr);
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

		Console.WriteLine("Use default keys? (y/n): ");
		string choice = Console.ReadLine();

		if (choice?.ToLower() == "n")
		{
			Console.WriteLine("Enter your public key XML: ");
			string publicKeyXml = Console.ReadLine();
			rsa.SetPublicKey(publicKeyXml);

			Console.WriteLine("Enter your private key XML: ");
			string privateKeyXml = Console.ReadLine();
			rsa.SetPrivateKey(privateKeyXml);
		}

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