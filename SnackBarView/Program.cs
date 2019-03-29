using SnackBarServiceDAL.Interfaces;
using SnackBarServiceImplementDataBase;
using SnackBarServiceImplementDataBase.Implementations;
using SnackBarServiceImplementList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace SnackBarView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<DbContext, BarDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISnackService, SnackServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());


            return currentContainer;
        }
    }
}
