using DemoApi.Dto;
using DemoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using models;
using DemoApi.Repositories.DB;

namespace DemoApi.Repositories
{
    public class InformationRepo : IInformationRepo
    {
        private readonly Mongo DB = new("InfromationBlock");
        public async Task<InformationBlockDto> CreateInformationAsync(InformationBlockDto info)
        {
            InformationBlock Inform = new()
            {
                Body = info.Body,
                Id = Guid.NewGuid(),
                Title = info.Title
            };
            
            await DB.CreateAsync<InformationBlock>(Inform);

            return Inform.AsDto();
        }

        public async Task DeleteInformarionAsync(Guid Id)
        {
            await DB.DeleteAsync<InformationBlock>(Id);
        }

        public async Task<IEnumerable<InformationBlock>> GetInforamtionAsync()
        {

            return await DB.FindAsync<InformationBlock>();

        }

        public async Task<InformationBlockDto> GetInformationByIdAsync(Guid Id)
        {
            
            return (await DB.FindByIdAsync<InformationBlock>(Id)).AsDto();
        }

        public async Task UpdateInformationAsync(Guid Id, InformationBlockDto info)
        { 

            InformationBlock InfoBlock = new()
            {
                Id = Id,
                Title = info.Title,
                Body = info.Body
            };

            await DB.UpdateAsync<InformationBlock>(InfoBlock, Id);
        }

    }
}
