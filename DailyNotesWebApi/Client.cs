using System;
using System.Collections.Generic;

namespace DailyNotesWebApi;

public partial class Client
{
    public int ClientId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int GenderId { get; set; }

    public virtual Gender ClientNavigation { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
