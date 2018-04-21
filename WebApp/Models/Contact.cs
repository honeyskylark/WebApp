using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Имя\"")]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Телефон\"")]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Компания\"")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
