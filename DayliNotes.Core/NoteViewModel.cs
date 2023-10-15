using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayliNotes.Core
{
    public class NoteViewModel
    {
        public int? NoteId { get; set; } = 0;

        public int ClientId { get; set; }

        public int NoteTypeId { get; set; }

        public string? NoteText { get; set; }

        public DateTime EditDate { get; set; }

        public string? NoteTitle { get; set; }
    }
}
