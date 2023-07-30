using SchoolSchedule.Domain.Filters.Schedule;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface IScheduleService
{
    public Task<DataTableResult<IEnumerable<ScheduleViewModel>>> GetSchedules(ScheduleFilter filter);

    public Task<IBaseResponse<ScheduleEditViewModel>> Create(ScheduleEditViewModel model);

    public Task<IBaseResponse<ScheduleEditViewModel>> Delete(ScheduleEditViewModel model);

    public Task<IBaseResponse<ClassRoomViewModel>> GetRarelyVisitedClassRoom(List<string> selectedWeekDays);

    public Task<IBaseResponse<IEnumerable<TeacherAndLessonCountViewModel>>> GetTeacherAndLessonCountByWeek();

    public Task<IBaseResponse<IEnumerable<ClassAndTeacherViewModel>>> GetTeacherAndLessonByClass(ClassAndTeacherViewModel model);
}