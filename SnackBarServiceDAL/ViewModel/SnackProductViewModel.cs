using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    public class SnackProductViewModel
    {
        public string НазваниеПродукта { get; set; }

        public int Id { get; set; }

        public int SnackId { get; set; }

        public int ProductId { get; set; }

        public int Количество { get; set; }
    }
}
