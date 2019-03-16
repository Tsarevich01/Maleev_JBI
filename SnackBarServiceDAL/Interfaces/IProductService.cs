using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetList();

        ProductViewModel GetElement(int id);

        void AddElement(ProductBindingModel model);

        void UpdElement(ProductBindingModel model);

        void DelElement(int id);
    }
}
