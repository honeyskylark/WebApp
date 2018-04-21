using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Deal
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Процесс\"")]
        public int? ProcessId { get; set; }
        public Process Process { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Клиент\"")]
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Заказал\"")]
        public int? FromId { get; set; }
        public From From { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Товар\"")]
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Назначен\"")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
