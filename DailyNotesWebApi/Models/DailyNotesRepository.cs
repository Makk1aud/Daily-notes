using Microsoft.EntityFrameworkCore;

namespace DailyNotesWebApi.Models
{
    public class DailyNotesRepository : IDailyNotesRepository
    {
        private readonly DailyNotesContext _context;
        public DailyNotesRepository(DailyNotesContext context)
        {
            _context= context;
        }
        public async Task<Client> CreateClient(Client client)
        {
            var result = await _context.Clients.AddAsync(client);
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

        public async Task<Note> UpdateNote(Note note)
        {
            var not = await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == note.NoteId);
            if (not != null)
            {
                not.NoteText = note.NoteText;
                not.NoteTitle = note.NoteTitle;
                not.EditDate = note.EditDate;
                not.NoteId= note.NoteId;
                not.NoteTypeId= note.NoteTypeId;
                not.ClientId = not.ClientId;
                await _context.SaveChangesAsync();
                return note;
            }
            return null;
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
        }
    }
}
