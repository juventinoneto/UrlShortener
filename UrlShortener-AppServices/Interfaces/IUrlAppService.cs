namespace UrlShortener_AppServices.Interfaces;

public interface IUrlAppService
{
    void CreateUrl(string url);

    string GetRawUrl(string key);

    string GetShortenedUrl(string originalUrl);
}
