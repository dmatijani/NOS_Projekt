using Microsoft.AspNetCore.Components;
using WebApp.Services;

namespace WebApp.Pages;

public partial class EncryptionAndDecryption
{
	[Inject]
	private IAesService AesService { get; set; }

	[Inject]
	private IRsaService RsaService { get; set; }

	[Inject]
	private IFileService FileService { get; set; }

	[Inject]
	private Global Global { get; set; }

	private bool _instantDownload
	{
		get => Global.InstantDownload;
		set => Global.InstantDownload = value;
	}

	private string _aesKey { get; set; } = string.Empty;
	private string _aesPlaintextFile { get; set; } = string.Empty;
	private string _aesEncryptedFile { get; set; } = string.Empty;
	private string _rsaPublicKey { get; set; } = string.Empty;
	private string _rsaPlaintextFile { get; set; } = string.Empty;
	private string _rsaPrivateKey { get; set; } = string.Empty;
	private string _rsaEncryptedFile { get; set; } = string.Empty;
	private string _result { get; set; } = string.Empty;
	private string _error { get; set; } = string.Empty;

	public async Task AesEncrypt()
	{
		try
		{
			_error = string.Empty;
			_result = AesService.Encrypt(_aesPlaintextFile, _aesKey);
			if (_instantDownload)
			{
				await FileService.DownloadText("aes_kriptirana.txt", _result);
			}
		} catch (Exception ex)
		{
			_result = string.Empty;
			_error = $"Greška: {ex.Message}";
		}
	}

	public async Task AesDecrypt()
	{
		try
		{
			_error = string.Empty;
			_result = AesService.Decrypt(_aesEncryptedFile, _aesKey);
			if (_instantDownload)
			{
				await FileService.DownloadText("aes_dekriptirana.txt", _result);
			}
		}
		catch (Exception ex)
		{
			_result = string.Empty;
			_error = $"Greška: {ex.Message}";
		}
	}

	public async Task RsaEncrypt()
	{
		try
		{
			_error = string.Empty;
			_result = RsaService.Encrypt(_rsaPlaintextFile, _rsaPublicKey);
			if (_instantDownload)
			{
				await FileService.DownloadText("rsa_kriptirana.txt", _result);
			}
		}
		catch (Exception ex)
		{
			_result = string.Empty;
			_error = $"Greška: {ex.Message}";
		}
	}

	public async Task RsaDecrypt()
	{
		try
		{
			_error = string.Empty;
			_result = RsaService.Decrypt(_rsaEncryptedFile, _rsaPrivateKey);
			if (_instantDownload)
			{
				await FileService.DownloadText("rsa_dekriptirana.txt", _result);
			}
		}
		catch (Exception ex)
		{
			_result = string.Empty;
			_error = $"Greška: {ex.Message}";
		}
	}
}
