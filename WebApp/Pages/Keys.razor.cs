using Microsoft.AspNetCore.Components;
using WebApp.Services;

namespace WebApp.Pages;

public partial class Keys
{
    [Inject]
    private IAesKeyGenerator AesKeyGenerator { get; set; }

    [Inject]
    private IRsaKeyGenerator RsaKeyGenerator { get; set; }

    [Inject]
    private IFileService FileService { get; set; }

    private bool _instantDownload = true;

    private string _aesKey = string.Empty;
    private string _rsaPublicKey = string.Empty;
    private string _rsaPrivateKey = string.Empty;

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
