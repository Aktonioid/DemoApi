using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("files")]
    [Authorize(Roles = "Admin, User")]
    public class MaterialsController:ControllerBase
    {
        private readonly IMaterialsRepo repository;

        private readonly IWebHostEnvironment env;
        public MaterialsController(IMaterialsRepo repository,IWebHostEnvironment env )
        {
            this.repository = repository;
            this.env = env;
        }

        [HttpGet("")] 
        public async Task<IEnumerable<MaterialsGroupDto>> GetMaterialsGroup()
        {
            var materialsGroup = await repository.GetMaterialsGroupAsync();
            
            return materialsGroup;
        }
    }
}
