using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FACTSERP.Models
{
    public class stockOuts
    {
        [Key]
        public int id { get; set; }

        
        public int productId { get; set; }

        [NotMapped]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        public int qty { get; set; }
    }
}
