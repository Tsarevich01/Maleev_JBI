using SnackBarModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Client> Clients { get; set; }

        public List<Product> Products { get; set; }

        public List<Order> Orders { get; set; }

        public List<Snack> Snacks { get; set; }

        public List<SnackProduct> SnackProducts { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Products = new List<Product>();
            Orders = new List<Order>();
            Snacks = new List<Snack>();
            SnackProducts = new List<SnackProduct>();
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
