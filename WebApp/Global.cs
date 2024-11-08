namespace WebApp;

/// <summary>
/// Klasa koja čuva informacije stalne u cijeloj aplikaciji
/// </summary>
public class Global
{
	public bool InstantDownload { get; set; }
	public string AesKey { get; set; } = string.Empty;
	public string RsaPublicKey { get; set; } = string.Empty;
	public string RsaPrivateKey { get; set; } = string.Empty;
}
