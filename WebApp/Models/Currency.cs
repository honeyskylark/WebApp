using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Name { get; set; }

    }
}
