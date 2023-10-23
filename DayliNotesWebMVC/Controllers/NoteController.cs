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
                JArray result = JArray.Parse(data);
                var notes = result.ToObject<List<NoteViewModel>>();

                //var listNotes = result["items"].Value<JArray>();
                //List<NoteViewModel> notes = listNotes.ToObject<List<NoteViewModel>>();


                //var listNotes = JsonConvert.DeserializeObject(data);
                //var notes = ((JArray)listNotes).Select(x => new NoteViewModel
                //{
                //    NoteId = (int)x["noteId"],
                //    ClientId = (int)x["ClientId"],
                //    NoteTypeId = (int)x["noteTypeId"],
                //    NoteText = (string)x["noteText"],
                //    EditDate = (DateTime)x["editDate"],
                //    NoteTitle = (string)x["noteTitle"]
                //}).ToList();
                
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
        public async Task<IActionResult> CreateNote(string? NoteText, string? NoteTitle, int ClientId)
        {
            if (NoteText == null)
                return View(ClientId);
            HttpResponseMessage response = await _client.PostAsJsonAsync(baseAddres + "/DailyNotes/CreateNote", new NoteViewModel()
            {
                ClientId = ClientId,
                NoteTypeId = 1,
                NoteText = NoteText,
                EditDate = DateTime.Now,
                NoteTitle = NoteTitle
            });
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Notes", new { clientId = ClientId });
            }
            return View(ClientId);
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
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNote(int noteId, int _clientId)
        {
            var response = await _client.DeleteAsync(baseAddres + "/DailyNotes/DeleteNote/" + noteId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Notes", new { clientId = _clientId });
            }
            return NotFound();
        }
    }
}
