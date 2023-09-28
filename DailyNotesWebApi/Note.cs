using System;
using System.Collections.Generic;

namespace DailyNotesWebApi;

public partial class Note
{
    public int NoteId { get; set; }

    public int ClientId { get; set; }

    public int NoteTypeId { get; set; }

    public string? NoteText { get; set; }

    public DateTime EditDate { get; set; }

    public string? NoteTitle { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual NoteType NoteType { get; set; } = null!;
}
