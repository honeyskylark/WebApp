using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Ключ\"")]
        public string Key { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить \"Значение\"")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Язык\"")]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
