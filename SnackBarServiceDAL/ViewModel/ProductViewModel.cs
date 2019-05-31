using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class ProductViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string НазваниеПродукта { get; set; }
    }
}
