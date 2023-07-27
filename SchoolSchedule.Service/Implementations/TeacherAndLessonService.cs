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

    public async Task<IBaseResponse<IEnumerable<TeacherAndLesson>>> GetTeacher(int teacherId)
    {
        try
        {
            var teacherAndLesson = await _teacherAndLessonRepository.GetAll()
                .Include(x=>x.Teacher)
                .Include(x=>x.LessonNameNavigation)
                .Where(x=>x.TeacherId == teacherId).ToListAsync();
            if (!teacherAndLesson.Any())
            {
                return new BaseResponse<IEnumerable<TeacherAndLesson>>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Teacher and lesson not found"
                };
            }

            return new BaseResponse<IEnumerable<TeacherAndLesson>>
            {
                Data = teacherAndLesson,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<TeacherAndLesson>>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherAndLessonService.[GetTeacher] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<TeacherAndLessonViewModel>> CreatePair(TeacherAndLessonViewModel model)
    {
        try
        {
            var teacherAndLesson = await _teacherAndLessonRepository.GetAll()
                .FirstOrDefaultAsync(x => x.LessonName == model.LessonName
                                          && x.TeacherId == model.TeacherId);
            if (teacherAndLesson != null)
            {
                return new BaseResponse<TeacherAndLessonViewModel>
                {
                    StatusCode = StatusCode.HasAlready,
                    Description = "This pair is has already"
                };
            }

            teacherAndLesson = new TeacherAndLesson()
            {
                TeacherId = (byte)model.TeacherId,
                LessonName = model.LessonName,
            };

            await _teacherAndLessonRepository.CreateAsync(teacherAndLesson);
            return new BaseResponse<TeacherAndLessonViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = $"You create new pair => [{teacherAndLesson.TeacherId}]-[{teacherAndLesson.LessonName}]"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<TeacherAndLessonViewModel>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherAndLessonService.[CreatePair] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<TeacherAndLessonViewModel>> DeletePair(TeacherAndLessonViewModel model)
    {
        try
        {
            var teacherAndLesson = await _teacherAndLessonRepository.GetAll()
                .FirstOrDefaultAsync(x=>x.TeacherId == model.TeacherId 
                                        && x.LessonName == model.LessonName);
            if (teacherAndLesson == null)
            {
                return new BaseResponse<TeacherAndLessonViewModel>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Teacher and lesson not found"
                };
            }

            await _teacherAndLessonRepository.RemoveAsync(teacherAndLesson);
            return new BaseResponse<TeacherAndLessonViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = $"You delete pair => [{teacherAndLesson.TeacherId}]-[{teacherAndLesson.LessonName}]"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<TeacherAndLessonViewModel>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherAndLessonService.[DeletePair] => {e.Message}"
            };
        }
    }
}