using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Database.Entities;
using SchoolSchedule.DAL.Interfaces;
using SchoolSchedule.Domain.Enum;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.Service.Implementations;

public class LessonService : ILessonService
{
    private readonly IBaseRepository<Lesson> _lessonRepository;

    public LessonService(IBaseRepository<Lesson> lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Lesson>>> GetLessons()
    {
        try
        {
            var lessons = await _lessonRepository.GetAll()
                .OrderBy(x => x.ClassRoom).ToListAsync();
            if (!lessons.Any())
            {
                return new BaseResponse<IEnumerable<Lesson>>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = $"Lessons not found"
                };
            }

            return new BaseResponse<IEnumerable<Lesson>>()
            {
                Data = lessons,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Lesson>>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[LessonService].[GetLessons] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<LessonViewModel>> AddLesson(LessonViewModel model)
    {
        try
        {
            var lesson = await _lessonRepository.GetAll()
                .FirstOrDefaultAsync(x => x.LessonName == model.LessonName);
            if (lesson != null)
            {
                return new BaseResponse<LessonViewModel>()
                {
                    StatusCode = StatusCode.HasAlready,
                    Description = $"Lesson {model.LessonName} is has already"
                };
            }

            var newLesson = new Lesson
            {
                LessonName = model.LessonName,
                ClassRoom = model.ClassRoom
            };
            await _lessonRepository.CreateAsync(newLesson);

            return new BaseResponse<LessonViewModel>()
            {
                StatusCode = StatusCode.OK,
                Description = $"You add new lesson - {newLesson.LessonName} in {newLesson.ClassRoom}"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<LessonViewModel>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[LessonService].[AddLesson] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<LessonViewModel>> Delete(LessonViewModel model)
    {
        try
        {
            var lessons = await _lessonRepository.GetAll()
                .OrderBy(x => x.ClassRoom).ToListAsync();
            int number = 0;

            foreach (var lesson in lessons)
            {
                if (++number == model.Id)
                {
                    await _lessonRepository.RemoveAsync(lesson);
                    return new BaseResponse<LessonViewModel>()
                    {
                        StatusCode = StatusCode.OK,
                        Description = $"You remove lesson - {lesson.LessonName}"
                    };
                }
            }

            return new BaseResponse<LessonViewModel>()
            {
                StatusCode = StatusCode.NotFound,
                Description = $"Lessons not found"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<LessonViewModel>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"[LessonService].[Delete] => {e.Message}"
            };
        }
    }
}