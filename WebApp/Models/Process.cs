using System.ComponentModel.DataAnnotations;
namespace WebApp.Models
{
    public class Process
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Порядок\"")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Title { get; set; }

    }
}
