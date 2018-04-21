using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Имя\"")]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
