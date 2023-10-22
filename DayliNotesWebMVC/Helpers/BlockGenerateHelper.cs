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
            string noteTitle = string.Empty;
            string result = string.Empty;
            foreach (Note note in listNotes)
            {
                noteTitle = note.NoteTitle != null? note.NoteTitle : "Без названия";
                if (!string.IsNullOrEmpty(note.NoteText))
                    noteText = note.NoteText.Length > maxLength ? note.NoteText.Substring(0, maxLength) + "..." : note.NoteText;
                result += $"<div class=\"box\">\r\n    " +
                    $"<p class='noteTitle'>{noteTitle}</p>\r\n    " +
                    $"<p class='noteText'>{noteText}</p>\r\n\r\n      " +
                    $"<p class='noteText'>{note.EditDate}</p>\r\n\r\n  " +
                    $"<form method='get' action='UpdateNote' >" +
                    $"<input type='number' name='noteId' hidden value='{note.NoteId}'/>" +
                    $"<p><input type='submit' class='btn-new' value='Редактировать'/></p></form>" +
                    $"<form action='DeleteNote'>" +
                    $"<input type='number' name='noteId' hidden value='{note.NoteId}'/>" +
                    $"<input type='number' name='_clientId' hidden value='{note.ClientId}'/>" +
                    $"<p><input type='submit' class='btn-new' value='Удалить'/></p></form>" +
                    $"</div>";
                noteText = string.Empty;
            }
            return new HtmlString(result);
        }
    }
}
