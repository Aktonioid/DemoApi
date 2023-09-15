using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using models;

namespace DemoApi.Controllers
{   //Контроллер сам ничего не выполняет по сути, он вызывает методы из NewsRepo
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepo repository;

        public NewsController(INewsRepo repository)
        {          
            this.repository = repository;
        }

        // GET /news/
        [HttpGet]
        public async Task<IEnumerable<NewsDto>> GetNews() //По идее тут должно быть GetNewsAsync, но если изменить имя,
        {                                                 //то не получается создать url при создании записи в БД
            var news = (await repository.GetNewsAsync()).Select(news => news.AsDto());
            
            return news;
        }

        // GET /news/{id}
        [HttpGet("/{newsid}")]
        public async Task<ActionResult<NewsDto>> GetNewsByIdAsync(Guid newsid)
        {
            var news = await repository.LoadRecordByIDAsync(newsid);
            
            if (news is null)
            {
                return NotFound();
            }
            
            return news.AsDto();

        }

    }
}
