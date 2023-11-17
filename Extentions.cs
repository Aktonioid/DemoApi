using DemoApi.Dto;
using dotenv.net;
using models;

namespace DemoApi
{
    public static class Extentions
    {
        public static NewsDto AsDto(this News news) 
        {
            return new NewsDto
            {
                Id = news.Id,
                Body = news.Body,
                CreatedAt = news.CreatedAt,
                ImageUrl = news.ImageUrl,
                Title = news.Title
            };
        }
        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                first_name = user.first_name,
                last_Name = user.last_Name,
                Id = user.Id,
                Kind = user.Kind,
                Login = user.Login,
                Password = user.Password,
            };
        }
        public static ScheduleDto AsDto(this Schedule schedule) 
        {
            return new ScheduleDto
            {
                Body = schedule.Body,
                Id = schedule.Id,
                key = schedule.key
            };
        }
        public static InformationBlockDto AsDto(this InformationBlock info)
        {
            return new InformationBlockDto
            {
                Body = info.Body,
                Id = info.Id,
                Title = info.Title
            };
        }
        public static MaterialsGroupDto AsDto(this MaterialsGroup mat)
        {
            return new MaterialsGroupDto
            {
                Files = mat.Files,
                Title = mat.Title,
                Id = mat.Id
            };
        }
        public static MaterialsFileDto AsDto(this MaterialsFile mat)
        {
            return new MaterialsFileDto
            {
                Id = mat.Id,
                Title = mat.Title,
                Url= mat.Url
            };
        }
        public static TaskDto AsDto(this TaskModel task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                CreatedAt = task.CreatedAt,
                Title = task.Title,
                Tasks = task.Tasks
            };
        }
        public static IDictionary<string, string> EnvDict()
        {
            return DotEnv.Read(options: new DotEnvOptions(envFilePaths: new[] { "settings.env" }));
        }
        public static ScheduleEvent AsReg(this ScheduleEventDto ev)
        {
            return new ScheduleEvent
            {
                Id = ev.Id,
                ToTime = ev.ToTime,
                FromTime = ev.FromTime,
                Name = ev.Name,
                Kind = ev.Kind                         
            };
        }
    }
}
