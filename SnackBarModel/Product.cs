using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackBarModel
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<SnackProduct> SnackProducts { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<StockProduct> StockProducts { get; set; }
    }
}
