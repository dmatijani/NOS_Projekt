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

    [Inject]
    private Global Global { get; set; }

    private async Task GenerateAesKey()
    {
        Global.AesKey = AesKeyGenerator.GenerateKey();
        if (Global.InstantDownload)
        {
			await FileService.DownloadText("tajni_kljuc.txt", Global.AesKey);
		}
    }
    
    private async Task GenerateRsaKeys()
    {
        var _rsaKeys = RsaKeyGenerator.GenerateKeyPair();
        Global.RsaPublicKey = _rsaKeys.PublicKey;
        Global.RsaPrivateKey = _rsaKeys.PrivateKey;
        if (Global.InstantDownload)
        {
			await FileService.DownloadText("javni_kljuc.txt", Global.RsaPublicKey);
			await FileService.DownloadText("privatni_kljuc.txt", Global.RsaPrivateKey);
		}
    }
}
