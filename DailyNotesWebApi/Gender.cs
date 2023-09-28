using System;
using System.Collections.Generic;

namespace DailyNotesWebApi;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderTitle { get; set; } = null!;

    public virtual Client? Client { get; set; }
}
