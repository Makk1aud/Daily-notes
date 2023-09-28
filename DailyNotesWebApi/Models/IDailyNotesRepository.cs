namespace DailyNotesWebApi.Models
{
    public interface IDailyNotesRepository
    {
        public Task<List<Note>> GetNotesByClientId(int clientId);
        public Task<List<Client>> GetClients();
        public Task<List<Note>> GetNotesByTitle(string noteTitle);
        public Task<Client> CreateClient(Client client);
        public Task<Note> UpdateNote(Note note);
        public Task<Client> GetClientById(int clientId);
        public Task<Note> GetNoteById(int noteId);
    }
}
