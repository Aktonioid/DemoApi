using System;
using DemoApi.Repositories.DB;
using DemoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using models;
using DemoApi.Dto;
using dotenv.net;

namespace DemoApi.Repositories
{
    public class NewsRepo : INewsRepo
    {

        readonly Mongo DB = new("News");

        private static IDictionary<string, string> EnvDict()
        {
            return DotEnv.Read(options: new DotEnvOptions(envFilePaths: new[] { "settings.env" }));
        }

        public async Task<IEnumerable<NewsDto>> GetNewsAsync()
        {
            return (await DB.FindAsync<News>()).Select(news => news.AsDto());            
            
        }

        public async Task<NewsDto> LoadRecordByIDAsync(Guid newsid)
        {
            return (await DB.FindByIdAsync<News>(newsid)).AsDto();
        }

        public async Task<NewsDto> CreateNewsAsync(NewsDto newsDto, IFormFile attachment, IWebHostEnvironment env) 
        {
            string path = env.ContentRootPath + "pictures";

            var filename = attachment.FileName;

            News news = new()
            {
                Id = Guid.NewGuid(),
                Body = newsDto.Body,
                CreatedAt = DateTime.Now,
                ImageUrl = path + filename,
                Title = newsDto.Title
            };

            using(var stream = System.IO.File.Create(path+filename))
            {
                attachment.CopyTo(stream);
                stream.Flush();
            }

            await DB.CreateAsync<News>(news);
            
            return news.AsDto();
        }

        public async Task UpdateNewsAsync(Guid id, NewsDto news, IFormFile attachment, IWebHostEnvironment env)
        {
            string path = env.ContentRootPath + "pictures";

            var oldNews = await DB.FindByIdAsync<News>(id);

            NewsDto updatable = new()
            {
                Id = id,
                Title = oldNews.Title,
                Body = oldNews.Body,
                ImageUrl = oldNews.ImageUrl,
                CreatedAt = oldNews.CreatedAt
            };

            var filename = "";

            if(attachment != null)
            {
                filename = attachment.FileName;
                updatable.ImageUrl = path + "/" + filename;
            }

            if(news.Body != null) updatable.Body = news.Body;
            
            if(news.Title != null) updatable.Title = news.Title;

            await DB.UpdateAsync<News>(updatable,id);
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            
            await DB.DeleteAsync<News>(id);
        }
    }
}
