using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using models;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepo repository;
        public ScheduleController(IScheduleRepo repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<ScheduleDto>> ScheduleGet()
        {
            return (await repository.GetSchedulesAsync());
        }
        
    }
}
