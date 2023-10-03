using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayliNotes.Core
{
    public class ClientViewModel
    {
        //public int ClientId { get; set; }
        [Required(ErrorMessage = "Введите имя пользователя!")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public int GenderId { get; set; }
    }
}
