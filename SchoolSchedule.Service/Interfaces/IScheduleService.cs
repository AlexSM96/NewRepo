using SchoolSchedule.Domain.Filters.Schedule;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface IScheduleService
{
    public Task<DataTableResult<IEnumerable<ScheduleViewModel>>> GetSchedules(ScheduleFilter filter);

    public Task<IBaseResponse<ScheduleEditViewModel>> CreateOneDay(ScheduleEditViewModel model);

    public Task<IBaseResponse<ScheduleEditViewModel>> DeleteOneDay(ScheduleEditViewModel model);

    public Task<IBaseResponse<ScheduleEditViewModel>> EditOneDay(ScheduleEditViewModel model);
}