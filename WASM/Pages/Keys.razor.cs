using Microsoft.AspNetCore.Components;
using WASM.Services;

namespace WASM.Pages;

public partial class Keys
{
    [Inject]
    private IAesKeyGenerator AesKeyGenerator { get; set; }

    [Inject]
    private IRsaKeyGenerator RsaKeyGenerator { get; set; }

    [Inject]
    private IFileService FileService { get; set; }

    [Inject]
    private Global Global { get; set; }

	private bool _instantDownload
	{
		get => Global.InstantDownload;
		set => Global.InstantDownload = value;
	}

	private string _aesKey
	{
		get => Global.AesKey;
		set => Global.AesKey = value;
	}

	private string _rsaPublicKey
	{
		get => Global.RsaPublicKey;
		set => Global.RsaPublicKey = value;
	}

	private string _rsaPrivateKey
	{
		get => Global.RsaPrivateKey;
		set => Global.RsaPrivateKey = value;
	}

	private async Task GenerateAesKey()
    {
        _aesKey = AesKeyGenerator.GenerateKey();
        if (_instantDownload)
        {
			await FileService.DownloadText("tajni_kljuc.txt", _aesKey);
		}
    }
    
    private async Task GenerateRsaKeys()
    {
        var _rsaKeys = RsaKeyGenerator.GenerateKeyPair();
        _rsaPublicKey = _rsaKeys.PublicKey;
        _rsaPrivateKey = _rsaKeys.PrivateKey;
        if (_instantDownload)
        {
			await FileService.DownloadText("javni_kljuc.txt", _rsaPublicKey);
			await FileService.DownloadText("privatni_kljuc.txt", _rsaPrivateKey);
		}
    }
}
