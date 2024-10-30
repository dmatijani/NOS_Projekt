namespace Application.Models;

public record AesKeyIv
{
    public string Key { get; set; }

    public string Iv { get; set; }
}
