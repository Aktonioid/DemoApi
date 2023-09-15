using models;
using DemoApi.Dto;

namespace DemoApi.Repositories
{
    public interface INewsRepo
    {
        //Здесь необходимо переделать News в NewsDto

        Task<IEnumerable<NewsDto>> GetNewsAsync();
        Task<NewsDto> LoadRecordByIDAsync(Guid id);
        public Task<NewsDto> CreateNewsAsync(NewsDto news, IFormFile attachment, IWebHostEnvironment env);
        public Task UpdateNewsAsync(Guid id, NewsDto news, IFormFile attachment, IWebHostEnvironment env);
        public Task DeleteNewsAsync(Guid id);
    }
}