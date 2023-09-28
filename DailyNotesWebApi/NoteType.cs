using System;
using System.Collections.Generic;

namespace DailyNotesWebApi;

public partial class NoteType
{
    public int NoteTypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
