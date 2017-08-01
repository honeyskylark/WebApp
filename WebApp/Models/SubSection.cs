using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SubSection
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Name { get; set; }

        public int? SectionId { get; set; }
        public Section Section { get; set; }

    }
}
