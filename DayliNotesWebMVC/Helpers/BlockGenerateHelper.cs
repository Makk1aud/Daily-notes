using DailyNotesWebApi;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DayliNotesWebMVC.Helpers
{
    public static class BlockGenerateHelper
    {
        public static HtmlString BlockGenerate(this IHtmlHelper html, List<Note> listNotes)
        {
            int maxLength = 60;
            string noteText = string.Empty; 
            string result = string.Empty;
            foreach (Note note in listNotes)
            {
                if (!string.IsNullOrEmpty(note.NoteText))
                    noteText = note.NoteText.Length > maxLength ? note.NoteText.Substring(0, maxLength) : note.NoteText;
                result += $"<div class=\"box\">\r\n    " +
                    $"<p>{note.NoteTitle}</p>\r\n    " +
                    $"<p>{noteText}</p>\r\n\r\n      " +
                    $"<p >{note.EditDate}</p>\r\n\r\n  " +
                    $"<form method='get' action='UpdateNote' >" +
                    $"<input type='number' name='noteId' hidden value='{note.NoteId}'/>" +
                    $"<p><input type='submit' value='Редактировать'</p></form>" +
                    $"</div>";
                noteText = string.Empty;
            }
            return new HtmlString(result);
        }
    }
}
