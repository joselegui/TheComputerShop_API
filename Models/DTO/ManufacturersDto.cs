using System.ComponentModel.DataAnnotations;

namespace TheComputerShop.Models.DTO
{
    public class ManufacturersDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Codigo es obligatorio")]
        [MaxLength(4, ErrorMessage = "El codigo es de 4 digitos")]
        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
    }
}
