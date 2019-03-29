using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveSnackPrice(ReportBindingModel model);

        List<StocksLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(ReportBindingModel model);

        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
