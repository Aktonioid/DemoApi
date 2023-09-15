using DemoApi.Dto;
using models;

namespace DemoApi.Repositories
{
    public interface IMaterialsRepo
    {
        public Task<MaterialsGroupDto> CreateMaterialsGroupAsync(MaterialsGroupDto file);
        public Task DeleteMaterialsGroupAsync(Guid id);
        public Task DeleteMaterialsFileAsync(Guid groupId, string name);
        public Task<MaterialsFileDto> CreateMAterialsFileAsyncT(Guid GroupId, IFormFile file, IWebHostEnvironment env);
        public Task<IEnumerable<MaterialsGroupDto>> GetMaterialsGroupAsync();
        public Task<MaterialsGroupDto> GetMaterialsGroupAsyncById(Guid GroupId);
        // public Task<MaterialsFileDto> getMaterialsFileByName(Guid gropuId, string filename); 
        
        
    }
}
