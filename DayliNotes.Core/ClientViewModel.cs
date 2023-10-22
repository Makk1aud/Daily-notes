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
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(15, ErrorMessage = "Минимум {2}, максимум {1}", MinimumLength = 6)]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(15, ErrorMessage ="Минимум {2}, максимум {1}", MinimumLength =6)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage ="Введите email")]
        [EmailAddress(ErrorMessage = "Неправильный email")]
        public string Email { get; set; } = null!;
        public int GenderId { get; set; } = 3;
    }
}
