using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class UController : ControllerBase
    {
        private readonly IRepository<Url> _repository;
        public UController(IRepository<Url> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<ActionResult> LinkRedirect(string shortUrl)
        {
            Url url = await _repository.GetUrlByShortUrl(shortUrl);

            if (url == null)
                return BadRequest("Такая ссылка не найдена");

            _repository.RedirectUrl(url);

            return Redirect(url.LongUrl);
        }
    }
}
