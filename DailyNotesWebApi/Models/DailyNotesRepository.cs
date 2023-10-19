using Microsoft.EntityFrameworkCore;
using DayliNotes.Core;

namespace DailyNotesWebApi.Models
{
    public class DailyNotesRepository : IDailyNotesRepository
    {
        private readonly DailyNotesContext _context;
        public DailyNotesRepository(DailyNotesContext context)
        {
            _context= context;
        }
        public async Task<Client> CreateClient(ClientViewModel client)
        {
            var newClient = new Client()
            {
                Password= client.Password,
                GenderId= client.GenderId,
                Login = client.Login,
                Email = client.Email
            };
            var result = await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<List<Client>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<List<Note>> GetNotesByTitle(string noteTitle)
        {
            return await _context.Notes.Where(x => x.NoteTitle.ToLower().StartsWith(noteTitle.ToLower())).ToListAsync();
        }

        public async Task<List<Note>> GetNotesByClientId(int clientId)
        {
            return await _context.Notes.Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task<Note> UpdateNote(NoteViewModel note)
        {
            var updNote = await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == note.NoteId);
            if (updNote != null)
            {
                updNote.NoteText = note.NoteText;
                updNote.NoteTitle = note.NoteTitle;
                updNote.EditDate = note.EditDate;
                updNote.NoteId= (int)note.NoteId;
                updNote.NoteTypeId= note.NoteTypeId;
                updNote.ClientId = note.ClientId;
                await _context.SaveChangesAsync();
                return updNote;
            }
            return null;
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
        }

        public async Task<Client> GetClientByLogin(string clientLogin)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Login == clientLogin);
        }
        public async Task<Note> CreateNote(NoteViewModel note)
        {
                Note newNote = new Note()
                {
                    ClientId = note.ClientId,
                    NoteTypeId = note.NoteTypeId,
                    NoteTitle = note.NoteTitle,
                    NoteText = note.NoteText,
                    EditDate = note.EditDate
                };
                var result = await _context.Notes.AddAsync(newNote);
                await _context.SaveChangesAsync();
                return result.Entity;
        }

        public async Task<Note> DeleteNote(Note note)
        {
            var result = _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
