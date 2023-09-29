using DailyNotesWebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DayliNotesWebMVC.Controllers
{
    public class AuthorisationController : Controller
    {
        private readonly ILogger<AuthorisationController> _logger;
        private readonly HttpClient _client;
        public Uri baseAddres = new Uri("https://localhost:7210/api");
        public AuthorisationController(ILogger<AuthorisationController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string? login, string? password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Поля пустые или пользователь не найден!";
                return View();
            }
            Client client = new Client();
            HttpResponseMessage response = _client.GetAsync(baseAddres + "/DailyNotes/GetClientByLogin/"+login).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;  
                client = JsonConvert.DeserializeObject<Client>(data);
                if (client.Password == password)
                    return Ok("Вошел");
            }
            return View();
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    List<Client> clients = new List<Client>();
        //    HttpResponseMessage response = _client.GetAsync(baseAddres + "/DailyNotes/GetClients").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        clients = JsonConvert.DeserializeObject<List<Client>>(data);
        //    }
        //    return View(clients);
        //}
    }
}
