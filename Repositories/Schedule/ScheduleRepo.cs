using DemoApi.Dto;
using DemoApi.Repositories;
using DemoApi.Repositories.DB;
using DemoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using models;

namespace DemoApi.Repositories
{
    public class ScheduleRepo : IScheduleRepo
    {
        private string Table = "Schedule";
        private readonly Mongo DB = new Mongo("Schedule");

        //Здесь вспомогательная чать для CreateSchedule

        private async Task<Schedule> LoadSchedByKeyAsync(int key)
        {
            try
            {
                await DB.FindByKeyAsync<Schedule>(key);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return await DB.FindByKeyAsync<Schedule>(key);
        }

        private async Task<Schedule> NoSchedAsync(int key, ScheduleEventDto ev) 
        {
            ScheduleEvent sched_event = new() 
            {
                Id = Guid.NewGuid(),
                FromTime = ev.FromTime,
                ToTime = ev.ToTime,
                Kind = ev.Kind,
                Name = ev.Name
            };


            List<ScheduleEvent> list = new List<ScheduleEvent>();
            list.Add(sched_event);

            Schedule sched = new() { Body = list, key = key, Id = Guid.NewGuid()};
            
            await DB.CreateAsync<Schedule>(sched);
            return sched;
        }
        
        //Конец Вспомогательной части
        public async Task<List<ScheduleEventDto>> CreateScheduleAsync(int key, List<ScheduleEventDto> ev)
        { 
            Schedule ExhistSched = await LoadSchedByKeyAsync(key);
            if(ExhistSched == null)
            { 
                for(int i =0; i < ev.Count; i++)
                {
                    ev[i].Id = new Guid();
                }

                ScheduleDto sched = new ScheduleDto()
                {
                    Id = new Guid(),
                    key = key,
                    Body = ev.Select(s => s.AsReg()).ToList()
                };

                await createScheduleAsync(sched);
                return ev;
            }

            for(int i =0; i < ev.Count; i++)
            {
                ev[i].Id = new Guid();
            }

            ExhistSched.Body = ev.Select(s => s.AsReg()).ToList();
            await DB.UpdateByKeyAsync(ExhistSched, key);
            return ev;
        }

        public async Task<List<ScheduleEventDto>> UpdateScheduleAsync(int key_dict, List<ScheduleEventDto> ev)
        {
            
            var mongolist = await DB.FindByKeyAsync<Schedule>(key_dict); //Массив вытянутый из mongo            
            
            Schedule sched = new() 
            {
                Id = mongolist.Id,
                key = mongolist.key,
                Body = ev.Select(s => s.AsReg()).ToList()
            };
            
            await DB.UpdateByKeyAsync<Schedule>(sched,key_dict);
            return ev;
        }
        
        public async  Task DeleteDayAsync(int key_dict)
        {
            var mongolist = await DB.FindByKeyAsync<Schedule>(key_dict);
            
            List<ScheduleEvent> lst = new();
            
            Schedule delete = new() 
            {
                Id = mongolist.Id,
                Body = lst,
                key = mongolist.key,

            };
            
            await DB.UpdateByKeyAsync<Schedule>(delete,key_dict);
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesAsync()
        {
            var schedule = await (DB.FindAsync<Schedule>());
            
            return schedule.Select(s => s.AsDto());
        }

        public async Task<ScheduleDto> createScheduleAsync(ScheduleDto scheduleDto)
        {
            Schedule schedule = new Schedule
            {
                Id = new Guid(),
                Body = scheduleDto.Body,
                key = scheduleDto.key
            };
            await DB.CreateAsync<Schedule>(schedule);
            return schedule.AsDto();
        }
    }
}
