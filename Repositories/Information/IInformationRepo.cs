using DemoApi.Dto;
using models;

namespace DemoApi.Repositories
{
    public interface IInformationRepo
    {
        public Task<IEnumerable<InformationBlock>> GetInforamtionAsync();
        public Task<InformationBlockDto> CreateInformationAsync(InformationBlockDto info);
        public Task UpdateInformationAsync(Guid Id, InformationBlockDto info);
        public Task DeleteInformarionAsync(Guid Id);
        public Task<InformationBlockDto> GetInformationByIdAsync(Guid Id);
    }
}
