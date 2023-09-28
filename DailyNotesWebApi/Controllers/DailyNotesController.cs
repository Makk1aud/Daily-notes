using DailyNotesWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyNotesWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DailyNotesController : ControllerBase
    {
        private readonly IDailyNotesRepository _dailyNotesRepository;
        public DailyNotesController(IDailyNotesRepository dailyNotesRepository)
        {
            _dailyNotesRepository = dailyNotesRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            try
            {
                var clients = await _dailyNotesRepository.GetClients();
                if(clients != null) 
                    return Ok(clients);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            try
            {
                var result = await _dailyNotesRepository.GetClientById(client.ClientId);
                if (result != null)
                    return Ok(result);
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            try
            {
                var result = await _dailyNotesRepository.GetClientById(id);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{noteTitle}")]
        public async Task<ActionResult> GetNotesByTitle(string noteTitle)
        {
            try
            {
                var result = await _dailyNotesRepository.GetNotesByTitle(noteTitle);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetNotesByClientId(int id)
        {
            try
            {
                var result = await _dailyNotesRepository.GetNotesByClientId(id);
                if (result != null) 
                    return Ok(result);
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Note>> UpdateNote(int id, Note note)
        {
            try
            {
                if (id != note.NoteId)
                    return BadRequest("Id must be equals!");

                var updNote = await _dailyNotesRepository.GetNoteById(id);
                if (updNote == null) 
                    return NotFound();
                return await _dailyNotesRepository.UpdateNote(updNote);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
