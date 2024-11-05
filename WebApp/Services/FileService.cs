using Microsoft.JSInterop;

namespace WebApp.Services;

public class FileService : IFileService
{
    private readonly IJSRuntime _jsRuntime;

    public FileService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task DownloadText(string filename, string content)
    {
        byte[] contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
        await Download(filename, contentBytes);
    }

    public async Task Download(string filename, byte[] content, string contentType = "text/plain")
    {
        await _jsRuntime.InvokeVoidAsync("Download", filename, content, contentType);
    }
}
