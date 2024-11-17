using Microsoft.AspNetCore.Components;
using WASM.Services;

namespace WASM.Pages;

public partial class Hashing
{
	[Inject]
	private IHashService HashService { get; set; }

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
	private string _result = string.Empty;
	private string _error = string.Empty;

	public async Task UploadFile()
	{
		_plaintextFile = await FileService.UploadText();
	}

	public async Task CalculateHash()
	{
		try
		{
			_error = string.Empty;
			_result = HashService.ComputeHash(_plaintextFile);
			if (_instantDownload)
			{
				await FileService.DownloadText("sazetak_datoteke.txt", _result);
			}
		} catch (Exception ex)
		{
			_result = string.Empty;
			_error = $"Greška: {ex.Message}";
		}
	}
}
