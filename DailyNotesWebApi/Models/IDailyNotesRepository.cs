using DayliNotes.Core;

namespace DailyNotesWebApi.Models
{
    public interface IDailyNotesRepository
    {
        public Task<List<Note>> GetNotesByClientId(int clientId);
        public Task<List<Client>> GetClients();
        public Task<List<Note>> GetNotesByTitle(string noteTitle);
        public Task<Client> CreateClient(ClientViewModel client);
        public Task<Note> UpdateNote(NoteViewModel note);
        public Task<Client> GetClientById(int clientId);
        public Task<Note> GetNoteById(int noteId);
        public Task<Client> GetClientByLogin(string clientLogin);
        public Task<Note> CreateNote(NoteViewModel note);
        public Task<Note> DeleteNote(Note note);
    }
}
