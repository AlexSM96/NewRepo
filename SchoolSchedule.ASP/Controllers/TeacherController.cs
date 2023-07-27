using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers;

public class TeacherController : Controller
{
    private readonly ITeacherService _service;

    public TeacherController(ITeacherService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers()
    {
        var response = await _service.GetTeachers();
        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher(TeacherViewModel model)
    {
        var response = await _service.AddTeacher(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int teacherId)
    {
        var response = await _service.Delete(teacherId);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Desription = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeacherViewModel model)
    {
        var response = await _service.Update(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }
}