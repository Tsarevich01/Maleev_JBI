using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class SnackViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string НазваниеЗакуски { get; set; }

        [DataMember]
        public decimal Цена { get; set; }

        [DataMember]
        public List<SnackProductViewModel> SnackProducts { get; set; }
    }
}
