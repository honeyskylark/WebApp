using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Catalog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Подраздел\"")]
        public int? SubSectionId { get; set; }
        public SubSection SubSection { get; set; }
    }
}
