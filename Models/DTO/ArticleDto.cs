using System.ComponentModel.DataAnnotations;

namespace TheComputerShop.Models.DTO
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Codigo es obligatorio")]
        public int Code { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El número maximo de caracteres es de 60!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public int Price { get; set; }
    }
}
