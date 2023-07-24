using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Entities;
using SchoolSchedule.DAL.Repository.Base;
using SchoolSchedule.Domain.Enum;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.Service.Implementations;

public class TeacherAndLessonService : ITeacherAndLessonService
{
    private readonly IBaseRepository<TeacherAndLesson> _teacherAndLessonRepository;

    public TeacherAndLessonService(IBaseRepository<TeacherAndLesson> teacherAndLessonRepository)
    {
        _teacherAndLessonRepository = teacherAndLessonRepository;
    }

    public async Task<IBaseResponse<IEnumerable<TeacherAndLesson>>> GetLessons(int teacherId)
    {
        try
        {
            var teacherAndLesson = await _teacherAndLessonRepository.GetAll()
                .Include(x=>x.Teacher)
                .Where(x=>x.TeacherId == teacherId).ToListAsync();
            if (!teacherAndLesson.Any())
            {
                return new BaseResponse<IEnumerable<TeacherAndLesson>>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Teacher and lesson not found"
                };
            }

            return new BaseResponse<IEnumerable<TeacherAndLesson>>()
            {
                Data = teacherAndLesson,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<TeacherAndLesson>>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherAndLessonService.[GetLessons] => {e.Message}"
            };
        }
    }

    public Task<IBaseResponse<TeacherAndLessonViewModel>> AddPair(TeacherAndLessonViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IBaseResponse<TeacherAndLessonViewModel>> DeletePair(TeacherAndLessonViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IBaseResponse<TeacherAndLessonViewModel>> UpdatePair(TeacherAndLessonViewModel model)
    {
        throw new NotImplementedException();
    }
}