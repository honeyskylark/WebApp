using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить \"Имя\"")]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Телефон\"")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Сообщение\"")]
        public string Message { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
