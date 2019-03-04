using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceDAL.BindingModel
{
    public class JbiBindingModel
    {
        public int Id { get; set; }
        public string JbiName { get; set; }
        public decimal Price { get; set; }
        public List<JbiSostavBindingModel> JbiSostav { get; set; }
    }
}
