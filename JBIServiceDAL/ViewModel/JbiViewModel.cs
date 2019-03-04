using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceDAL.ViewModel
{
    public class JbiViewModel
    {
        public int Id { get; set; }
        public string JbiName { get; set; }
        public decimal Price { get; set; }
        public List<JbiSostavViewModel> JbiSostavs { get; set; }
    }
}
