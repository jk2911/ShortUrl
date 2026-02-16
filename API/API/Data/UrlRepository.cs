using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UrlRepository : IRepository<Url>
    {
        private MySQLDataContext _context;
        public UrlRepository(MySQLDataContext context) 
        {
            _context = context;
        }
        public void Create(Url item)
        {
            _context.URLs.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Url item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public async Task<Url> Get(int id)
        {
            return await _context.URLs.
                FirstOrDefaultAsync(url => url.Id == id);
        }

        public async Task<IEnumerable<Url>> GetAll()
        {
            return await _context.URLs.ToListAsync();
        }

        public async Task<string?> GetLongUrl(string shortUrl)
        {
            var url = await _context.URLs.
                Where(u =>  u.ShortUrl == shortUrl).
                FirstOrDefaultAsync();

            return url == null ? null : url.LongUrl;
        }

        public async Task<Url> GetUrlByShortUrl(string shortUrl)
        {
            return await _context.URLs.
                Where(u => u.ShortUrl == shortUrl).
                FirstOrDefaultAsync();
        }

        public async Task<bool> IsLinkCreated(string shortUrl)
        {
            var url = await _context.URLs.
                Where(u => u.ShortUrl == shortUrl).
                FirstOrDefaultAsync();

            return url != null;
        }

        public void RedirectUrl(Url item)
        {
            item.CountClick++;
            Update(item);

        }

        public void Update(Url item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
