using JBIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Client> Clients { get; set; }

        public List<Sostav> Sostavs { get; set; }

        public List<Order> Orders { get; set; }

        public List<Jbi> Jbis { get; set; }

        public List<JbiSostav> JbiSostavs { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Sostavs = new List<Sostav>();
            Orders = new List<Order>();
            Jbis = new List<Jbi>();
            JbiSostavs = new List<JbiSostav>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
