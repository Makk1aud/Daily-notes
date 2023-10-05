using DailyNotesWebApi;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DayliNotesWebMVC.Helpers
{
    public static class BlockGenerateHelper
    {
        public static HtmlString BlockGenerate(this IHtmlHelper html, List<Note> listNotes)
        {
            int maxLength = 80;
            string noteText = string.Empty; 
            string result = string.Empty;
            foreach (Note note in listNotes)
            {
                if (!string.IsNullOrEmpty(note.NoteText))
                    noteText = note.NoteText.Length > maxLength ? note.NoteText.Substring(0, maxLength) : note.NoteText;
                result += $"<div class=\"box\">" +
                    $"<p>{note.NoteTitle}</p>" +
                    $"<p>{noteText}...</p>" +
                    $"<p>{note.EditDate}</p>" +
                    $"</div>";
                noteText = string.Empty;
            }
            return new HtmlString(result);
        }
    }
}
