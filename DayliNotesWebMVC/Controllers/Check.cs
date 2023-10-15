using DayliNotesWebMVC.Classes;
using Microsoft.AspNetCore.Mvc;

namespace DayliNotesWebMVC.Controllers
{
    public class Check : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            GetResponseNote.ResponseNote();
            return View();
        }
    }
}
