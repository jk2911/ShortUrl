namespace API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        Task<string?> GetLongUrl(string shortUrl);
        void RedirectUrl (T item);
        Task<T> GetUrlByShortUrl(string shortUrl);
    }
}
