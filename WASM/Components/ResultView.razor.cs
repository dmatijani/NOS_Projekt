using Microsoft.AspNetCore.Components;
using WASM.Services;

namespace WASM.Components;

public partial class ResultView
{
	[Parameter]
	public string ButtonText { get; set; }

	[Parameter]
	public string Result { get; set; }

	[Parameter]
	public EventCallback<string> ResultChanged { get; set; }

	[Parameter]
	public string Error { get; set; }

	[Parameter]
	public EventCallback<string> ErrorChanged { get; set; }

	[Parameter]
	public EventCallback Action { get; set; }

	[Parameter]
	public bool ActionEnabled { get; set; } = true;

	[Parameter]
	public string FileName { get; set; } = "datoteka.txt";

	[Inject]
	private IFileService FileService { get; set; }

	public async Task ClearResult()
	{
		Result = string.Empty;
		await ResultChanged.InvokeAsync(Result);
		Error = string.Empty;
		await ErrorChanged.InvokeAsync(Error);
	}

	public async Task SaveResult()
	{
		if (!string.IsNullOrEmpty(Error))
		{
			return;
		}

		await FileService.DownloadText(FileName, Result);
	}
}
