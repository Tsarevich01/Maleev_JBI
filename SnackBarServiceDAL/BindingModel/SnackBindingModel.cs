using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.BindingModel
{
    public class SnackBindingModel
    {
        public int Id { get; set; }
        public string SnackName { get; set; }
        public decimal Price { get; set; }
        public List<SnackProductBindingModel> SnackProduct { get; set; }
    }
}
