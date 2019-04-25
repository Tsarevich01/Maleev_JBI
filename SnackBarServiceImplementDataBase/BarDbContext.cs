using SnackBarModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceImplementDataBase
{
    public class BarDbContext : DbContext
    {
        public BarDbContext() : base("BarDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Snack> Snacks { get; set; }
        public virtual DbSet<SnackProduct> SnackProducts { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockProduct> StockProducts { get; set; }
        
    }
}
