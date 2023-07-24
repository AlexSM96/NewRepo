using SchoolSchedule.DAL.Entities;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface ILessonService
{
    public Task<IBaseResponse<IEnumerable<Lesson>>> GetLessons();

    public Task<IBaseResponse<LessonViewModel>> AddLesson(LessonViewModel model);

    public Task<IBaseResponse<LessonViewModel>> Delete(LessonViewModel model);
}