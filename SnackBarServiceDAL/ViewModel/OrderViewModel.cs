using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ФИОЗаказчика { get; set; }

        public int SnackId { get; set; }

        public string НазваниеЗакуски { get; set; }

        public int Количество { get; set; }

        public decimal Сумма { get; set; }

        public string Статус { get; set; }

        public string ДатаЗаказа { get; set; }

        public string ДатаВыполнения { get; set; }
    }
}
