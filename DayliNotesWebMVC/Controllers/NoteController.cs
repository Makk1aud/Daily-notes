using Microsoft.AspNetCore.Mvc;

namespace DayliNotesWebMVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly HttpClient _client;
        public Uri baseAddres = new Uri("https://localhost:7210/api");
        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }

        public async Task<IActionResult> Notes(int clientId)
        {
            return Ok(clientId.ToString());
        }
    }
}
