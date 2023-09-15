using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("information")]
    public class InformationBlockController : ControllerBase
    {
        private readonly IInformationRepo repository;
 
        public InformationBlockController(IInformationRepo repository)
        {
            this.repository = repository;
        } 

        [HttpGet]
        public async Task<IEnumerable<InformationBlockDto>> GetInformation()
        {
            var information = (await repository.GetInforamtionAsync()).Select(info => info.AsDto());
            
            return information;
        }

        [HttpGet("{info_id}")]
        public async Task<ActionResult<InformationBlockDto>> GetByIdAsync(Guid info_id)
        {

            return await repository.GetInformationByIdAsync(info_id);
        }
        
    }
}
