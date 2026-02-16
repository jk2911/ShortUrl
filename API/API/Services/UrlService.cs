using API.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class UrlService : IUrlService
    {
        public async Task<string> CreateShortUrl(string url)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(url + DateTime.Now.ToString()))).Substring(0, 5);
        }
    }
}
