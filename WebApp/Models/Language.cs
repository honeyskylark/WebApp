using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Name { get; set; }
    }
}
