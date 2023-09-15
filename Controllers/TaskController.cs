using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepo repository;

        public TaskController(ITaskRepo repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<TaskDto>> GetTask()
        {
            var Tasks = await repository.GetTasksAsync();
            
            return Tasks;
        }
        [HttpGet("{task_id}")]
        public async Task<ActionResult<TaskDto>> GetTaskByIdAsync(Guid task_id)
        {
            var task = await repository.GetTaskByIdAsync(task_id);
            
            return task;
        }
        
    }
}
