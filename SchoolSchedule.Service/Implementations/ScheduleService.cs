using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Entities;
using SchoolSchedule.DAL.Repository.Base;
using SchoolSchedule.Domain.Enum;
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

    private async Task ChangeModelData(ScheduleEditViewModel model)
    {
        model.WeekDay = await _scheduleRepository.GetAll()
            .Where(x => x.WeekDayNavigation.WeekDayName == model.WeekDayName)
            .Select(x => x.WeekDay)
            .FirstOrDefaultAsync();

        model.TeacherId = await _scheduleRepository.GetAll()
            .Where(x => x.TeacherAndLesson.Teacher.FullName == model.TeacherName)
            .Select(x => x.TeacherId)
            .FirstOrDefaultAsync();
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

    public async Task<IBaseResponse<ScheduleEditViewModel>> CreateOneDay(ScheduleEditViewModel model)
    {
        try
        {
            var schedule = await _scheduleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.ClassId == model.ClassId
                                          && x.LessonNumber == model.LessonNumber
                                          && x.LessonName == model.LessonName);

            if (schedule != null)
            {
                return new BaseResponse<ScheduleEditViewModel>
                {
                    StatusCode = StatusCode.HasAlready,
                    Description = "This time-slot is already taken"
                };
            }

            await ChangeModelData(model);

            schedule = new Schedule
            {
                WeekDay = model.WeekDay,
                ClassId = model.ClassId,
                LessonNumber = (byte)model.LessonNumber,
                TeacherId = (byte)model.TeacherId,
                LessonName = model.LessonName
            };

            await _scheduleRepository.CreateAsync(schedule);

            return new BaseResponse<ScheduleEditViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = "You add new schedule day"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ScheduleEditViewModel>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[ScheduleService.CreateOneDay] => {e.Message}"
            };
        }
    }


    public async Task<IBaseResponse<ScheduleEditViewModel>> DeleteOneDay(ScheduleEditViewModel model)
    {
        try
        {
            var schedule = await _scheduleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.WeekDay == model.WeekDay
                                          && x.ClassId == model.ClassId
                                          && x.LessonNumber == model.LessonNumber
                                          && x.TeacherId == model.TeacherId);
            if (schedule == null)
            {
                return new BaseResponse<ScheduleEditViewModel>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Schedule not found"
                };
            }

            await _scheduleRepository.RemoveAsync(schedule);
            return new BaseResponse<ScheduleEditViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = "You remove schedule day"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ScheduleEditViewModel>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[ScheduleService.DeleteOneDay] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<ClassRoomViewModel>> GetRarelyVisitedClassRoom()
    {
        try
        {
            var classRoom = _scheduleRepository.GetAll()
                .GroupBy(x => x.TeacherAndLesson.LessonNameNavigation.ClassRoom)
                .Select(x => new ClassRoomViewModel
                {
                    ClassRoom = x.Key,
                    VisitCount = x.Count()
                })
                .OrderBy(x=>x.VisitCount)
                .ThenBy(x=>x.ClassRoom)
                .FirstOrDefault();

            if (classRoom == null)
            {
                return new BaseResponse<ClassRoomViewModel>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Classroom not found"
                };
            }

            return new BaseResponse<ClassRoomViewModel>
            {
                StatusCode = StatusCode.OK,
                Data = classRoom
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ClassRoomViewModel>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[ScheduleService.GetRarelyVisitedClassRoom] => {e.Message}"
            };
        }
    }
}