using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.BindingModel
{
    [DataContract]
    public class StockBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StockName { get; set; }
    }
}
