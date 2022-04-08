using System.ComponentModel.DataAnnotations;

namespace FACTSERP.Models
{
    public class products
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int qty { get; set; }
    }
}
