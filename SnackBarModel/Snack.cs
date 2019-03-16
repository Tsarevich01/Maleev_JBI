using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackBarModel
{
    public class Snack
    {
        public int Id { get; set; }

        [Required]
        public string SnackName { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("SnackId")]
        public virtual List<Order> Orders { get; set; }
    }
}
