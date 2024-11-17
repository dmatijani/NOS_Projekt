using Microsoft.AspNetCore.Components;
using WASM.Services;

namespace WASM.Pages;

public partial class DigitalSignature
{
    [Inject]
    private IDigitalSignatureService DigitalSignatureService { get; set; }

    [Inject]
    private IFileService FileService { get; set; }

    [Inject]
    private Global Global { get; set; }

    private bool _instantDownload
    {
        get => Global.InstantDownload;
        set => Global.InstantDownload = value;
    }

    private string _plaintextFile = string.Empty;
    private string _rsaPrivateKey = string.Empty;
    private string _digitalSignatureFile = string.Empty;
    private string _rsaPublicKey = string.Empty;
    private string _signatureResult = string.Empty;
    private bool? _verifyResult = null;
    private string _error = string.Empty;

	public async Task UploadFile()
	{
		_plaintextFile = await FileService.UploadText();
	}

    public async Task UploadRsaPrivateKey()
    {
        _rsaPrivateKey = await FileService.UploadText();
    }

	public async Task Sign()
    {
        try
        {
            _error = string.Empty;
            _signatureResult = DigitalSignatureService.Sign(_plaintextFile, _rsaPrivateKey);
            if (_instantDownload)
            {
                await FileService.DownloadText("digitalni_potpis.txt", _signatureResult);
            }
        } catch (Exception ex)
        {
            _signatureResult = string.Empty;
            _error = $"Greška: {ex.Message}";
        }
    }

    public async Task Verify()
    {
        try
        {
            _error = string.Empty;
            _verifyResult = DigitalSignatureService.Verify(_plaintextFile, _digitalSignatureFile, _rsaPublicKey);
        } catch (Exception ex)
        {
            _verifyResult = null;
            _error = $"Greška: {ex.Message}";
        }
    }

    public void ClearResult()
    {
        _error = string.Empty;
        _verifyResult = null;
    }
}
