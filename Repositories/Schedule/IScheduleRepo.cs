using DemoApi.Dto;
using models;

namespace DemoApi.Repositories
{
    public interface IScheduleRepo
    {

        public Task<List<ScheduleEventDto>> CreateScheduleAsync(int key, List<ScheduleEventDto> ev);
        public Task<ScheduleDto> createScheduleAsync(ScheduleDto scheduleDto);
        public Task<List<ScheduleEventDto>> UpdateScheduleAsync(int key ,List<ScheduleEventDto> ev);
        public Task DeleteDayAsync(int key);
        public Task<IEnumerable<ScheduleDto>> GetSchedulesAsync(); 
    }
}
