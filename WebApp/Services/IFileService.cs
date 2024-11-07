namespace WebApp.Services;

public interface IFileService
{
    Task DownloadText(string filename, string content);

    Task Download(string filename, byte[] content, string contentType = "text/plain");

    Task<string> UploadText();

    Task<string> Upload(string allowed);
}
