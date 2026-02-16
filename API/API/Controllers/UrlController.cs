using API.Controllers.Base;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UrlController : BaseApiController
    {
        private readonly IRepository<Url> _repository;
        private readonly IUrlService _urlService;

        public UrlController(IRepository<Url> repository, IUrlService urlService) 
        {
            _repository = repository;
            _urlService = urlService;
        }

        [HttpGet]
        [Route("GetUrls")]
        public async Task<IEnumerable<Url>> GetUrls()
        {
            return await _repository.GetAll();
        }
        [HttpPost]
        [Route("AddUrl")]
        public async Task<ActionResult> AddNewUrl(string longUrl)
        {
            string shortUrl;

            do
            {
                shortUrl = await _urlService.CreateShortUrl(longUrl);

            } while ( await _repository.IsLinkCreated(shortUrl));

            var newUrl = new Url()
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                DateCreate = DateTime.Now,
            };

            _repository.Create(newUrl);

            return Ok("Короткая ссылка создана");
        }
        [HttpDelete]
        [Route("DeleteUrl")]
        public async Task<ActionResult> DeleteUrl(int id)
        {
            var url = await _repository.Get(id);

            if(url == null)
                return NotFound("Ссылка не найдена");

            _repository.Delete(url);

            return Ok("Ссылка удалена");
        }
        [HttpPut]
        [Route("EditUrl")]
        public async Task<ActionResult> EditUrl(int id, string newLongUrl)
        {
            var url = await _repository.Get(id);

            if (url == null)
                return BadRequest("Ссылка не найдена");

            url.LongUrl = newLongUrl;

            _repository.Update(url);

            return Ok("Ссылка изменена");
        }
    }
}
