using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers;

public class LessonController : Controller
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessons()
    {
        var response = await _service.GetLessons();
        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddLesson(LessonViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _service.AddLesson(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { Description = response.Description });
            }

            return BadRequest(new { Description = response.Description });
        }

        return BadRequest(ModelState);
        
    }

    [HttpPost]
    public async Task<IActionResult> DeleteLesson(LessonViewModel model)
    {
        var response = await _service.Delete(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }
}