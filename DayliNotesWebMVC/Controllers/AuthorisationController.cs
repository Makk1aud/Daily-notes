using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DayliNotesWebMVC.Controllers
{
    public class AuthorisationController : Controller
    {
        private readonly ILogger<AuthorisationController> _logger;

        public AuthorisationController(ILogger<AuthorisationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
