using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.BindingModel
{
    [DataContract]
    public class StockProductBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
