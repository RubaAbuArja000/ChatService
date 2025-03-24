using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username") ?? string.Empty;
            return View();
        }
    }
}
