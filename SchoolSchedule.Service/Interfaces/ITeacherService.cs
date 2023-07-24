using SchoolSchedule.DAL.Entities;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface ITeacherService
{
    public Task<IBaseResponse<IEnumerable<Teacher>>> GetTeachers();

    public Task<IBaseResponse<TeacherViewModel>> AddTeacher(TeacherViewModel model);

    public Task<IBaseResponse<Teacher>> Delete(int id);

    public Task<IBaseResponse<TeacherViewModel>> Update(TeacherViewModel model);
}