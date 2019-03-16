using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarModel
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string StockName { get; set; }

        [ForeignKey("StockId")]
        public virtual List<StockProduct> StockProducts { get; set; }
    }
}
