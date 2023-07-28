using Microsoft.AspNetCore.Mvc;

namespace SchoolSchedule.ASP.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult GetRequest()
        {
           return View();
        }
    }
}
