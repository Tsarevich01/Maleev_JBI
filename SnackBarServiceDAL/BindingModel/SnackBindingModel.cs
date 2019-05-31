using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.BindingModel
{
    [DataContract]
    public class SnackBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SnackName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<SnackProductBindingModel> SnackProduct { get; set; }
    }
}
