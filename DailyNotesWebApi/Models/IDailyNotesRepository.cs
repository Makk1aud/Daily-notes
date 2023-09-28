namespace DailyNotesWebApi.Models
{
    public interface IDailyNotesRepository
    {
        public Task<List<Note>> GetNotesByClientId(string clientId);
        public Task<List<Client>> GetClients();
        public Task<Note> GetNoteByTitle(string noteTitle);
        public Task<Client> CreateClient(Client client);
        public Task<Note> UpdateNote(Note note);
    }
}
