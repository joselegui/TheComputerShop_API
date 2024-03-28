using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheComputerShop.Models
{
    public class Articles
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


        [ForeignKey("CodeID")]
        public int CodeID { get; set; }
        public Manufacturers Manufacturers { get; set; }

    }
}
