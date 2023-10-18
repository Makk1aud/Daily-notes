using DailyNotesWebApi;
using DayliNotes.Core;
using DayliNotesWebMVC.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Text;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DayliNotesWebMVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly HttpClient _client;
        public Uri baseAddres = new Uri("https://localhost:7210/api");
        public static int _clientId;
        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }

        public async Task<IActionResult> Notes(int clientId)
        {
            HttpResponseMessage response = await _client.GetAsync(baseAddres + "/DailyNotes/GetNotesByClientId/" + clientId);
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();


                var listNotes = JsonConvert.DeserializeObject(data);
                List<Note> notes = ((JArray)listNotes).Select(x => new Note
                {
                    NoteId = (int)x["noteId"],
                    ClientId = (int)x["clientId"],
                    NoteTypeId = (int)x["noteTypeId"],
                    NoteText = (string)x["noteText"],
                    EditDate = (DateTime)x["editDate"],
                    NoteTitle = (string)x["noteTitle"]
                }).ToList();
                ViewBag.ClientId = clientId;
                return View(notes);
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult CreateNote(int clientId)
        {
            return View(clientId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(string? NoteText, string? NoteTitle, int clientId)
        {
            //string data = JsonConvert.SerializeObject(viewModel);
            //StringContent content = new StringContent(data.ToLower(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsJsonAsync(baseAddres + "/DailyNotes/CreateNote", new NoteViewModel()
            {
                ClientId = clientId,
                NoteTypeId = 1,
                NoteText = NoteText,
                EditDate = DateTime.Now,
                NoteTitle = NoteTitle
            });
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Notes", new { clientId = clientId });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNote(int noteId)
        {
            var response = await _client.GetAsync(baseAddres + "/DailyNotes/GetNoteById/" + noteId);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Note note = JsonConvert.DeserializeObject<Note>(data);
                return View(note);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNote(Note note)
        {
            var noteViewModel = new NoteViewModel()
            {
                NoteId = note.NoteId,
                ClientId = note.ClientId,
                NoteTypeId = 1,
                NoteText = note.NoteText,
                EditDate = DateTime.Now,
                NoteTitle = note.NoteTitle
            };

            var response = await _client.PutAsJsonAsync(baseAddres + "/DailyNotes/UpdateNote", noteViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Notes", new { clientId = note.ClientId });
            }
            return View();
        }
    }
}
