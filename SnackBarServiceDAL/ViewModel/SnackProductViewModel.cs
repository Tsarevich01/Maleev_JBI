using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class SnackProductViewModel
    {
        [DataMember]
        public string НазваниеПродукта { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int SnackId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Количество { get; set; }
    }

}
