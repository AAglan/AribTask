using Microsoft.AspNetCore.Mvc;

namespace AribTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Employee : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
