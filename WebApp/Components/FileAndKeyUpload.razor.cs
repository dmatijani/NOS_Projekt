using Microsoft.AspNetCore.Components;
using WebApp.Services;

namespace WebApp.Components;

public partial class FileAndKeyUpload
{
	[Inject]
	private IFileService FileService { get; set; }

	[Parameter]
	public string FileContent { get; set; }
	
	[Parameter]
	public EventCallback<string> FileContentChanged { get; set; }

	[Parameter]
	public string Key { get; set; }

	[Parameter]
	public EventCallback<string> KeyChanged { get; set; }

	[Parameter]
	public string FileTitle { get; set; } = "Datoteka: ";

	[Parameter]
	public string KeyTitle { get; set; } = "Ključ: ";

	public async Task UploadFile()
	{
		FileContent = await FileService.UploadText();
		await FileContentChanged.InvokeAsync(FileContent);
	}

	public async Task UploadKey()
	{
		Key = await FileService.UploadText();
		await KeyChanged.InvokeAsync(Key);
	}
}
