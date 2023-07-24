using SchoolSchedule.DAL.Entities;
using SchoolSchedule.Domain.Response;
using SchoolSchedule.Domain.ViewModels;

namespace SchoolSchedule.Service.Interfaces;

public interface IClassService
{
    public Task<IBaseResponse<IEnumerable<Class>>> GetClasses();

    public Task<IBaseResponse<ClassViewModel>> AddClass(ClassViewModel model);

    public Task<IBaseResponse<Class>> Delete(int id);

    public Task<IBaseResponse<ClassViewModel>> UpdateClass(ClassViewModel model, int id);
}