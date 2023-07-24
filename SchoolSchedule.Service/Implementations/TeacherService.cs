using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Entities;
using SchoolSchedule.DAL.Repository.Base;
using SchoolSchedule.Domain.Enum;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.Service.Implementations;

public class TeacherService : ITeacherService
{
    private readonly IBaseRepository<Teacher> _teacherRepository;

    public TeacherService(IBaseRepository<Teacher> teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Teacher>>> GetTeachers()
    {
        try
        {
            var teachers = await _teacherRepository.GetAll().ToListAsync();
            if (!teachers.Any())
            {
                return new BaseResponse<IEnumerable<Teacher>>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Teachers not found"
                };
            }

            return new BaseResponse<IEnumerable<Teacher>>()
            {
                Data = teachers,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Teacher>>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"Service.[GetTeachers] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<TeacherViewModel>> AddTeacher(TeacherViewModel model)
    {
        try
        {
            var teacher = await _teacherRepository.GetAll()
                .FirstOrDefaultAsync(x => x.FullName == model.TeacherName);
            if (teacher != null)
            {
                return new BaseResponse<TeacherViewModel>()
                {
                    StatusCode = StatusCode.HasAlready,
                    Description = $"Teacher [{teacher.FullName}] is has already"
                };
            }

            var newTeacher = new Teacher()
            {
                FullName = model.TeacherName
            };

            await _teacherRepository.CreateAsync(newTeacher);
            return new BaseResponse<TeacherViewModel>()
            {
                StatusCode = StatusCode.OK,
                Description = $"You add new teacher - [{newTeacher.FullName}]"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<TeacherViewModel>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherService.[AddTeacher] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Teacher>> Delete(int id)
    {
        try
        {
            var teacher = await _teacherRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return new BaseResponse<Teacher>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = $"Teacher ID: {id} not found"
                };
            }

            await _teacherRepository.RemoveAsync(teacher);
            return new BaseResponse<Teacher>()
            {
                StatusCode = StatusCode.OK,
                Description = $"You delete teacher - [{teacher.FullName}]"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Teacher>
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherService.[AddTeacher] => {e.Message}"
            };
        }
    }

    public async Task<IBaseResponse<TeacherViewModel>> Update(TeacherViewModel model)
    {
        try
        {
            var teacher = await _teacherRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (teacher == null)
            {
                return new BaseResponse<TeacherViewModel>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = $"Teacher [{model.Id}] - {model.TeacherName} not found"
                };
            }

            teacher.FullName = model.TeacherName;
            await _teacherRepository.UpdateAsync(teacher);
            return new BaseResponse<TeacherViewModel>()
            {
                StatusCode = StatusCode.OK,
                Description = $"Teacher ID: [{model.Id}] has updated"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<TeacherViewModel>()
            {
                StatusCode = StatusCode.ServerError,
                Description = $"TeacherService.[Update] => {e.Message}"
            };
        }
    }
}
