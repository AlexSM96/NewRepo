using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers;

public class ClassController :Controller
{
    private readonly IClassService _service;

    public ClassController(IClassService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetClasses()
    {
        var classes = await _service.GetClasses();
        return View(classes.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddClass(ClassViewModel model)
    {
        var response = await _service.AddClass(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveClass(int id)
    {
        var response = await _service.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateClass(ClassViewModel model, int id)
    {
        var response = await _service.UpdateClass(model, id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedule(int classId)
    {
        var response = await _service.GetSchedule(classId);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return BadRequest(new { Description = response.Description });
    }
}