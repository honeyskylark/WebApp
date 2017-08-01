using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Название\"")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Описание\"")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Цена\"")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Валюта\"")]
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Единица Измерения\"")]
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }

        public string Img { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить \"Каталог\"")]
        public int? CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }

}

