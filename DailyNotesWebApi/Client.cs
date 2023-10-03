using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyNotesWebApi;

public partial class Client
{
    public int ClientId { get; set; }
    [Required(ErrorMessage ="Введите имя пользователя!")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Введите пароль!")]
    public string Password { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public int GenderId { get; set; }

    //public virtual Gender ClientNavigation { get; set; } = null;

    public virtual ICollection<Note> Notes { get; set; } /*= new List<Note>();*/
}
