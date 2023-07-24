using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Entities;
using SchoolSchedule.DAL.Repository.Base;
using SchoolSchedule.Domain.Extensions;
using SchoolSchedule.Domain.Filters.Schedule;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.Service.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IBaseRepository<Schedule> _scheduleRepository;

    public ScheduleService(IBaseRepository<Schedule> scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<DataTableResult<IEnumerable<ScheduleViewModel>>> GetSchedules(ScheduleFilter filter)
    {
        try
        {
            var schedule = await GetFullSchedule(filter);
            var scheduleViewModel = schedule
                .Select(x => new ScheduleViewModel
                {
                    DayWeek = x.WeekDayNavigation.WeekDayName,
                    ClassName = x.Class.ClassName,
                    LessonNumber = x.LessonNumber,
                    LessonTimeStart = x.LessonNumberNavigation.LessonTimeStart.ToShortTimeString(),
                    LessonTimeEnd = x.LessonNumberNavigation.LessonTimeEnd.ToShortTimeString(),
                    LessonName = x.LessonName,
                    TeacherName = x.TeacherAndLesson.Teacher.FullName,
                    ClassRoom = x.TeacherAndLesson.LessonNameNavigation.ClassRoom
                })
                .Skip(filter.ItemForSkip)
                .Take(filter.PageSize);

            var count = schedule.Count();

            return new DataTableResult<IEnumerable<ScheduleViewModel>>
            {
                Data = scheduleViewModel,
                ItemsCount = count
            };
        }
        catch (Exception e)
        {
            return new DataTableResult<IEnumerable<ScheduleViewModel>>
            {
                Data = default,
                ItemsCount = default
            };
        }
    }

    private async Task<List<Schedule>> GetFullSchedule(ScheduleFilter filter)
    {
        return await _scheduleRepository.GetAll()
            .Include(x => x.WeekDayNavigation)
            .Include(x => x.LessonNumberNavigation)
            .Include(x => x.TeacherAndLesson.Teacher)
            .Include(x => x.TeacherAndLesson.LessonNameNavigation)
            .Include(x => x.Class)
            .WhereIf(!string.IsNullOrWhiteSpace(filter.ClassName), x => x.Class.ClassName == filter.ClassName)
            .ToListAsync();
    }
}