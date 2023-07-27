using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers;

public class TeacherAndLessonController : Controller
{
    private readonly ITeacherAndLessonService _service;

    public TeacherAndLessonController(ITeacherAndLessonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeacher(int id, bool isJson)
    {
        var response = await _service.GetTeacher(id);
        if (isJson)
        {
            return Json(response.Data);
        }

        return PartialView("GetTeacher", response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePair(TeacherAndLessonViewModel model)
    {
        var response = await _service.CreatePair(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    public async Task<IActionResult> DeletePair(TeacherAndLessonViewModel model)
    {
        var response = await _service.DeletePair(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }
}