using DemoApi.Dto;
using DemoApi.Repositories.DB;
using DemoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using models;

namespace DemoApi.Repositories
{
    public class TaskRepo : ITaskRepo
    {
        private readonly Mongo DB = new("Tasks");
        private readonly string connection = "Tasks";

        public async Task<Boolean> CreateElementAsync(Guid Id, TaskElementDto elem)
        {
            var exhistTask = await DB.FindByIdAsync<TaskModel>(Id);
            if(exhistTask == null)
            {
                return false;
            }
            TaskElement element = new()
            {
                Id = Guid.NewGuid(),
                Body = elem.Body,
                Class_Name = elem.Class_Name,
                UntilDate = elem.UntilDate
            };

            var list = exhistTask.Tasks;
            
            list.Add(element);

            exhistTask = exhistTask with { Tasks = list };
            
            await DB.UpdateAsync<TaskModel>(exhistTask,Id);
            return true;
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto task)
        {
            TaskModel Newtask = new() 
            {
                CreatedAt = DateTime.Now,
                TaskId = Guid.NewGuid(),
                Tasks = task.Tasks,
                Title = task.Title,
            };
           
            await DB.CreateAsync<TaskModel>(Newtask);
           
            return Newtask.AsDto();
        }

        public async Task DeleteTaskAsync(Guid Id)
        {
            await DB.DeleteAsync<TaskModel>(Id);
        }

        public async Task DeleteTaskElementAsync(Guid id, Guid ElId)
        {
            var exhistTask = await DB.FindByIdAsync<TaskModel>(id);
            var list = exhistTask.Tasks;
           
            for (int i = 0; i<list.Count; i++)
            {
                if (list[i].Id == ElId)
                {
                    list.RemoveAt(i);
                }
            }
           
            exhistTask = exhistTask with { Tasks = list };
           
            await DB.UpdateAsync<TaskModel>(exhistTask, id);
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid taskid)
        {
            var tasks =await DB.FindByIdAsync<TaskModel>(taskid) ;
            
            return tasks.AsDto();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync()
        {
            var Tasks = await DB.FindAsync<TaskModel>();
            
            return Tasks.Select(Task => Task.AsDto());
        }

        public async Task UpdateTaskElementAsync(Guid Id, Guid ElemId,TaskElementDto task)
        {
            var exhist = await DB.FindByIdAsync<TaskModel>(Id);
            var list = exhist.Tasks;
            
            for(int i = 0; i<list.Count; i++)
            {
                if (list[i].Id == ElemId)
                {
                    if (list[i].Class_Name != task.Class_Name)
                    {
                        list[i] = list[i] with { Class_Name = task.Class_Name};
                    }
                    if (list[i].Body != task.Body)
                    {
                        list[i] = list[i] with {  Body = task.Body};
                    }
                    break;
                }
            }
            
            exhist = exhist with { Tasks = list};
            
            await DB.UpdateAsync<TaskModel>(exhist, Id);
        }
    }
}
