using Microsoft.AspNetCore.Mvc;

namespace TamilMurasu.Controllers.User
{
    public class HeaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
