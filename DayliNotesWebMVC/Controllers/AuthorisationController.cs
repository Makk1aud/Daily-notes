using DailyNotesWebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using DayliNotes.Core;
using DayliNotesWebMVC.Classes;

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
                return View();
            Client client = new Client();
            HttpResponseMessage response = _client.GetAsync(baseAddres + "/DailyNotes/GetClientByLogin/"+login).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;  
                client = JsonConvert.DeserializeObject<Client>(data);
                if (client.Password == password)
                    return RedirectToAction("Notes", "Note", new { clientId = client.ClientId});
            }
            ViewBag.Error = "Неправильный логин или пароль";
            ViewBag.Login = login;
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(ClientViewModel client)
        {
            if(client == null)
                return View();
            //string data = JsonConvert.SerializeObject(client);
            //StringContent content = new StringContent(data.ToLower(), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = _client.PostAsync(baseAddres + "/DailyNotes/CreateClient", content).Result;
            HttpResponseMessage response = _client.PostAsJsonAsync(baseAddres + "/DailyNotes/CreateClient", client).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
