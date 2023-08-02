using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Service.Interfaces;

namespace SchoolSchedule.ASP.Controllers
{
    public class RequestController : Controller
    {
        private readonly IClassService _service;
        public RequestController(IClassService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetRequest()
        {
            var response = await _service.GetClasses();
            return View(response.Data);
        }
    }
}
