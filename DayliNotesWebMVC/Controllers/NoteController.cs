using DailyNotesWebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Text.Json.Nodes;

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
            HttpResponseMessage response = await _client.GetAsync(baseAddres + "/DailyNotes/GetNotesByClientId/" + clientId);
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                //JObject result = JObject.Parse(data);

                //var notesJS = result.Value<JArray>();
                //List<Note> notes = notesJS.ToObject<List<Note>>();
                //return View(notes);
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
                return View(notes);
            }
            return View(null);
        }
    }
}
