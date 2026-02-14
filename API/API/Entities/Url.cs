namespace API.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int CountClick { get; set; } = 0;
        public DateTime DateCreate { get; set; }
    }
}
