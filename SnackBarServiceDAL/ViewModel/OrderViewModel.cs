using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public string ФИОЗаказчика { get; set; }

        [DataMember]
        public int SnackId { get; set; }

        [DataMember]
        public string НазваниеЗакуски { get; set; }

        [DataMember]
        public int Количество { get; set; }

        [DataMember]
        public decimal Сумма { get; set; }

        [DataMember]
        public string Статус { get; set; }

        [DataMember]
        public string ДатаЗаказа { get; set; }

        [DataMember]
        public string ДатаВыполнения { get; set; }
    }
}
