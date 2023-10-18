using DailyNotesWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DayliNotes.Core;

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
        public async Task<ActionResult<Client>> CreateClient(ClientViewModel client)
        {
            try
            {
                var busyClient = await _dailyNotesRepository.GetClientByLogin(client.Login);
                if (busyClient == null)
                {
                    await _dailyNotesRepository.CreateClient(client);
                    return Ok(client);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
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

        [HttpPut]
        public async Task<ActionResult<Note>> UpdateNote(NoteViewModel note)
        {
            try
            {
                if (note == null)
                    return BadRequest("Note cant be null");
                var updNote = await _dailyNotesRepository.GetNoteById((int)note.NoteId);
                if (updNote == null) 
                    return NotFound();
                return await _dailyNotesRepository.UpdateNote(note);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{clientLogin}")]
        public async Task<ActionResult<Client>> GetClientByLogin(string clientLogin)
        {
            try
            {
                var client = _dailyNotesRepository.GetClientByLogin(clientLogin).Result;
                if (client != null)
                    return Ok(client);
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(NoteViewModel note)
        {
            try
            {
                if (note == null)
                    return BadRequest("Cant be Null");
                await _dailyNotesRepository.CreateNote(note);
                return Ok(note);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("{noteId:int}")]
        public async Task<ActionResult<Note>> GetNoteById(int noteId)
        {
            try
            {
                var note = await _dailyNotesRepository.GetNoteById(noteId);
                if(note == null)
                    return NotFound();
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
