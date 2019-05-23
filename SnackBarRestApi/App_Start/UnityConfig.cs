using SnackBarServiceDAL.Interfaces;
using SnackBarServiceImplementDataBase;
using SnackBarServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using Unity;
using Unity.Lifetime;

namespace SnackBarRestApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, BarDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IClientService, ClientServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IProductService, ProductServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<ISnackService, SnackServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IStockService, StockServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IReportService, ReportServiceDB>(new
           HierarchicalLifetimeManager());
        }
    }
}