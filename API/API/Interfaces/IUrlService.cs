namespace API.Interfaces
{
    public interface IUrlService
    {
        Task<string> CreateShortUrl(string url);
    }
}
