using DemoApi.Dto;
using models;

namespace DemoApi.Repositories
{
    public interface ITaskRepo
    {
        public Task<TaskDto> CreateTaskAsync(TaskDto task);
        public Task UpdateTaskElementAsync(Guid Id, Guid ElementId ,TaskElementDto task);
        public Task DeleteTaskElementAsync(Guid Id, Guid ElementId);
        public Task DeleteTaskAsync(Guid Id);
        public Task<TaskDto> GetTaskByIdAsync(Guid Id);
        public Task<IEnumerable<TaskDto>> GetTasksAsync();
        public Task<Boolean> CreateElementAsync(Guid Id, TaskElementDto elem);
    }
}
