using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using models;
using DemoApi;
using Microsoft.AspNetCore.Authorization;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        
        private readonly IUserRepo userRepo;
        
        private readonly IInformationRepo informationRepo;
        
        private readonly ITaskRepo taskRepo;
        
        private readonly IScheduleRepo schedRepo;
        
        private readonly IMaterialsRepo materialRepo;
        
        private readonly INewsRepo newsRepo;
        
        private readonly IWebHostEnvironment env;

        public AdminController(IUserRepo userRepo, IInformationRepo informationRepo, ITaskRepo taskRepo, IScheduleRepo schedRepo, IMaterialsRepo materialsRepo, INewsRepo newsRepo, IWebHostEnvironment env)
        {
            this.userRepo = userRepo;
            
            this.informationRepo = informationRepo;

            this.taskRepo = taskRepo;
            
            this.schedRepo = schedRepo;
            
            this.materialRepo = materialsRepo;
            
            this.newsRepo = newsRepo;

            this.env = env;
        }

        //Student
        [HttpGet("students")]
        public Task<IEnumerable<UserDto>> GetUsers()
        {
            var user = userRepo.GetUsersAsync();
            
            return user;
        }
        [HttpGet("students/{student_id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(string student_id)
        {
            Guid id = new Guid(student_id);
            var ExhistUser = await userRepo.GetUserByIdAsync(id);
            if (ExhistUser == null) return NoContent();

            return ExhistUser;
        }
        
        [HttpPost("students/create")]

        public async Task<ActionResult<UserDto>> createUserAsync([FromBody] UserDto userDto)
        {

            User user = await userRepo.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user.AsDto());
        }
        

        [HttpDelete("students/{student_id}/delete")]
        public async Task<ActionResult> DeleteUser(Guid student_id)
        {
            await userRepo.DeleteUserAsync(student_id);
            
            return NoContent();
        }
        [HttpPost("students/{student_id}/edit")]
        public async Task<ActionResult<UserDto>> EditUser(Guid student_id, [FromBody] UserDto userdto)
        {
            await userRepo.EditUserAsync(student_id, userdto);
            
            return NoContent();
        }

        //Information

        [HttpGet("info")]
        public async Task<IEnumerable<InformationBlockDto>> GetInformation()
        {
            var information = (await informationRepo.GetInforamtionAsync()).Select(info => info.AsDto());
            
            return information;
        }

        [HttpGet("info/{info_id}")]
        public async Task<ActionResult<InformationBlockDto>> GetByIdAsync(Guid info_id)
        {

            return await informationRepo.GetInformationByIdAsync(info_id);
        }
        
        [HttpPost("info/create")]
        public async Task<ActionResult<InformationBlockDto>> CreateInfoAsync([FromBody] InformationBlockDto info)
        {
            InformationBlockDto Information = await informationRepo.CreateInformationAsync(info);
            
            return CreatedAtAction(nameof(GetInformation), new { Id = Information.Id, }, Information);
        }
        
        [HttpPost("info/{id}/delete")]
        public async Task<ActionResult> deleteAsync(Guid id)
        {
            await informationRepo.DeleteInformarionAsync(id);
            return Ok();
        }

        [HttpPost("info/{Id}/update")]
        public async Task<ActionResult> UpdeteAsync(Guid Id, [FromBody] InformationBlockDto info)
        {
            await informationRepo.UpdateInformationAsync(Id, info);
            
            return Ok();
        }

        //Task
        [HttpGet("tasks")]
        public async Task<IEnumerable<TaskDto>> GetTask()
        {
            var Tasks = await taskRepo.GetTasksAsync();
            
            return Tasks;
        }

        [HttpGet("tasks/{task_id}")]
        public async Task<ActionResult<TaskDto>> GetTaskByIdAsync(Guid task_id)
        {
            var task = await taskRepo.GetTaskByIdAsync(task_id);
            
            return task;
        }

        [HttpPost("tasks/create/")]
        public async Task<ActionResult<TaskDto>> CreateTaskAsync([FromBody]TaskDto task)
        {
            var NewTask = await taskRepo.CreateTaskAsync(task);
         
            return CreatedAtAction(nameof(GetTask), new { Id = NewTask.TaskId }, NewTask);
        }

        [HttpPost("tasks/{id}/elements/create")]
        public async Task<ActionResult> CreateTaskElement(Guid Id, [FromBody]TaskElementDto task)
        {
            bool isTaskExhist = await taskRepo.CreateElementAsync(Id, task);
            if(isTaskExhist == false) return NotFound();
            return Ok(); 
        }

        [HttpPost("tasks/{id}/elements/{ElId}/update")]
        public async Task<ActionResult> UpdateTaskElement(Guid Id, Guid ElId, [FromBody]TaskElementDto task)
        {
            await taskRepo.UpdateTaskElementAsync(Id, ElId, task);
            
            return Ok();
        }

        [HttpDelete("tasks/delete/{id}")]
        public async Task<ActionResult> DeleteTask(Guid Id)
        {
            await taskRepo.DeleteTaskAsync(Id);
            
            return NoContent();
        }

        [HttpDelete("tasks/{id}/elements/{elid}/delete")]
        public async Task<ActionResult> DeleteTaskElement(Guid Id, Guid ElId)
        {
            await taskRepo.DeleteTaskElementAsync(Id, ElId);
            
            return NoContent();
        }

        //Schedule

        [HttpGet("schedule")]
        public async Task<IEnumerable<ScheduleDto>> ScheduleGet()
        {
            return (await schedRepo.GetSchedulesAsync()).Select(schedule => schedule.AsDto());
        }
        [HttpPost("schedule/createsched")]
        public async Task<ActionResult<ScheduleDto>> createSchedule(ScheduleDto scheduleDto)
        {
            ScheduleDto sched = await schedRepo.createScheduleAsync(scheduleDto);
            return Ok(sched);
        }

        [HttpPost("schedule/update/{key}")]
        public async Task<ActionResult<ScheduleEventDto>> Update(int key, int key_list, [FromBody]List<ScheduleEventDto> ev)
        {
            var ret = await schedRepo.UpdateScheduleAsync(key, ev);
           
            return Ok(ret);
        }

        [HttpPost("schedule/create/{key}")]
        public async Task<ActionResult> Create(int key, [FromBody]List<ScheduleEventDto> ev)
        {
            var ret = await schedRepo.CreateScheduleAsync(key, ev);

            return Ok(ret);
        }

        [HttpDelete("schedule/delete/{key}")]
        public async Task<ActionResult> Delete(int key)
        {
            await schedRepo.DeleteDayAsync(key);
            
            return NoContent();
        }

        //Materials


        [HttpGet("materials")]
        public async Task<IEnumerable<MaterialsGroupDto>> GetMaterialsGroup()
        {
            var materialsGroup = await materialRepo.GetMaterialsGroupAsync();
           
            return materialsGroup;
        }


        [HttpPost("materials/create")]
        public async Task<ActionResult<MaterialsGroupDto>> CreateGroupAsync([FromBody]MaterialsGroupDto Group)
        {
            var ret = await materialRepo.CreateMaterialsGroupAsync(Group);
         
            return CreatedAtAction(nameof(GetMaterialsGroup), new { Id = ret.Id }, ret);
        }


        [HttpPost("materials/create/{groupid}")]
        public async Task<ActionResult> CreateFile([FromForm]IFormFile attachment, Guid groupid)
        {

            var ret = await materialRepo.CreateMAterialsFileAsyncT(groupid, attachment, env);
         
            return CreatedAtAction(nameof(GetMaterialsGroup), new { Id = ret.Id }, ret);
        }

        [HttpDelete("materials/delete/{groupid}")]
        public async Task<ActionResult> DeleteGroup(Guid groupid)
        {
            await materialRepo.DeleteMaterialsGroupAsync(groupid);
         
            return NoContent();
        }

        [HttpDelete("materials/delete/{groupid}/{filename}")]
        public async Task<ActionResult> DeleteFile(Guid groupid, string filename)
        {
            await materialRepo.DeleteMaterialsFileAsync(groupid, filename);
         
            return NoContent();
        }

        // [HttpGet("materials/get/{groupid}/{filename}")]
        // public async Task<ActionResult<MaterialsFileDto>> getFileByFileName(string filename, Guid groupId)
        // {
        //     var returnable = await materialRepo.getMaterialsFileByName(groupId, filename);

        //     return returnable;
        // }


        //News
        [HttpGet("news")]
        public async Task<IEnumerable<NewsDto>> GetNews() //По идее тут должно быть GetNewsAsync, но если изменить имя,
        {                                                 //то не получается создать url при создании записи в БД
            var news = (await newsRepo.GetNewsAsync()).Select(news => news.AsDto());
            
            return news;
        }

        // GET /news/{id}
        [HttpGet("news/{newsid}")]
        public async Task<ActionResult<NewsDto>> GetNewsByIdAsync(Guid newsid)
        {
            var news = await newsRepo.LoadRecordByIDAsync(newsid);
            
            if (news is null)
            {
                return NotFound();
            }
            
            return news.AsDto();

        }

        [HttpPost("news/create")]

        [Authorize]
        public async Task<ActionResult<NewsDto>> CreateNewsAsync([FromForm]NewsDto newsDto, [FromForm] IFormFile attachment)
        {

            NewsDto news = await newsRepo.CreateNewsAsync(newsDto, attachment, env);


            return CreatedAtAction(nameof(GetNews), new { id = news.Id }, news.AsDto());
        }
        [HttpPost("news/{post_id}/edit")]

        public async Task<ActionResult<NewsDto>> UpdateNews(Guid post_id, [FromForm]NewsDto newsDto, [FromForm] IFormFile? attachment)
        {
            await newsRepo.UpdateNewsAsync(post_id, newsDto, attachment, env);
           
            return NoContent();
        }
        
        [HttpDelete("news/{post_id}/delete")]
        public async Task<ActionResult> DeleteNewsAsync(Guid post_id)
        {
            await newsRepo.DeleteNewsAsync(post_id);
           
            return NoContent();

        }
    }
}

