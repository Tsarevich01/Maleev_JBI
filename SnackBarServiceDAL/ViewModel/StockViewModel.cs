using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class StockViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string НазваниеСклада { get; set; }

        [DataMember]
        public List<StockProductViewModel> StockProducts { get; set; }
    }
}
