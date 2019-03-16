using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    public class SnackViewModel
    {
        public int Id { get; set; }
        public string НазваниеЗакуски { get; set; }
        public decimal Цена { get; set; }
        public List<SnackProductViewModel> SnackProducts { get; set; }
    }
}
