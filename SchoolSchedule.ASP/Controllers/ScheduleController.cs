using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Domain.Filters.Schedule;
using SchoolSchedule.Domain.ViewModels;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers;

public class ScheduleController : Controller
{
    private readonly IScheduleService _service;

    public ScheduleController(IScheduleService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetSchedules() => View();

    [HttpPost]
    public async Task<IActionResult> ScheduleHandler(ScheduleFilter filter)
    {
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var pageSize = length != null ? int.Parse(length) : default;
        var itemForSkip = start != null ? int.Parse(start) : default;
        filter.ItemForSkip = itemForSkip;
        filter.PageSize = pageSize;
        var schedule = await _service.GetSchedules(filter);
        return Json(new
        {
            Data = schedule.Data,
            RecordsTotal = schedule.ItemsCount,
            RecordsFiltered = schedule.ItemsCount
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleEditViewModel model)
    {
        var response = await _service.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new { Description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSchedule(ScheduleEditViewModel model)
    {
        var response = await _service.Delete(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { Description = response.Description });
        }

        return BadRequest(new {Description = response.Description});
    }

    public async Task<IActionResult> GetClassRoom(List<string> selectedWeekDays, bool isJson)
    {
        var response = await _service.GetRarelyVisitedClassRoom(selectedWeekDays);
        if (isJson)
        {
            return Json(new { Description = response.Description });
        }

        return PartialView("GetClassRoom", response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetTeacherAndLessonCountByWeek(bool isJson)
    {
        var response = await _service.GetTeacherAndLessonCountByWeek();
        if (isJson)
        {
            return Json(new { Description = response.Description });
        }

        return PartialView("GetTeacherAndLessonCountByWeek", response.Data);
    }

    public async Task<IActionResult> GetTeacherAndLessonByClass(string className, bool isJson)
    {
        var response = await _service.GetTeacherAndLessonByClass(className);
        if (isJson)
        {
            return Json(new { Description = response.Description });
        }

        return PartialView("GetTeacherAndLessonByClass", response.Data);
    }
}