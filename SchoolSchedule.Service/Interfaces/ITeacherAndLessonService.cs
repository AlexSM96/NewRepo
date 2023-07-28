using SchoolSchedule.DAL.Entities;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface ITeacherAndLessonService
{
    public Task<IBaseResponse<IEnumerable<TeacherAndLesson>>> GetTeacher(int teacherId);

    public Task<IBaseResponse<TeacherWithLessonCountViewModel>> GetTeacherWhoTeachesMostLessons();

    public Task<IBaseResponse<TeacherAndLessonViewModel>> CreatePair(TeacherAndLessonViewModel model);

    public Task<IBaseResponse<TeacherAndLessonViewModel>> DeletePair(TeacherAndLessonViewModel model);
}