using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Entities;
using SchoolSchedule.DAL.Repository.Base;
using SchoolSchedule.Domain.Enum;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.Service.Implementations;

public class ClassService : IClassService
{
    private readonly IBaseRepository<Class> _classRepository;

    public ClassService(IBaseRepository<Class> classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Class>>> GetClasses()
    {
        try
        {
            var classes = await _classRepository.GetAll().ToListAsync();

            if (classes != null)
            {
                return new BaseResponse<IEnumerable<Class>>()
                {
                    Data = classes,
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<IEnumerable<Class>>()
            {
                StatusCode = StatusCode.NotFound,
                Description = $"Classes is null"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Class>>()
            {
                Description = $"[Service.GetClasses] => {e.Message}",
                StatusCode = StatusCode.ServerError
            };
        }
    }


    public async Task<IBaseResponse<ClassViewModel>> AddClass(ClassViewModel model)
    {
        try
        {
            var classes = await _classRepository.GetAll()
                .FirstOrDefaultAsync(x => x.ClassName == model.ClassName);
            if (classes != null)
            {
                return new BaseResponse<ClassViewModel>
                {
                    StatusCode = StatusCode.HasAlready,
                    Description = $"Class is exist"
                };
            }

            var newClass = new Class
            {
                ClassName = model.ClassName
            };

            await _classRepository.CreateAsync(newClass);
            return new BaseResponse<ClassViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = $"Class {newClass.ClassName} created"
            };

        }
        catch (Exception e)
        {
            return new BaseResponse<ClassViewModel>
            {
                Description = $"[Service.AddClass] => {e.Message}",
                StatusCode = StatusCode.ServerError
            };
        }
    }

    public async Task<IBaseResponse<Class>> Delete(int id)
    {
        try
        {
            var selectedClass = await _classRepository.GetAll()
                .FirstOrDefaultAsync(x => x.ClassId == id);
            if (selectedClass == null)
            {
                return new BaseResponse<Class>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Class not found"
                };
            }

            await _classRepository.RemoveAsync(selectedClass);
            return new BaseResponse<Class>()
            {
                StatusCode = StatusCode.OK,
                Description = $"Class ID:{selectedClass.ClassId} Name:{selectedClass.ClassName} deleted"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Class>
            {
                Description = $"[Service.DeleteTeacher] => {e.Message}",
                StatusCode = StatusCode.ServerError
            };
        }
    }

    public async Task<IBaseResponse<ClassViewModel>> UpdateClass(ClassViewModel model, int id)
    {
        try
        {
            var updatedClass = await _classRepository.GetAll()
                .FirstOrDefaultAsync(x => x.ClassId == id);
            if (updatedClass == null)
            {
                return new BaseResponse<ClassViewModel>()
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Class not found"
                };
            }

            updatedClass.ClassName = model.ClassName;
            await _classRepository.UpdateAsync(updatedClass);
            return new BaseResponse<ClassViewModel>
            {
                StatusCode = StatusCode.OK,
                Description = $"Class edited - {updatedClass.ClassId}"
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ClassViewModel>
            {
                Description = $"[Service.UpdateClass] => {e.Message}",
                StatusCode = StatusCode.ServerError
            };
        }
    }
}