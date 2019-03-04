using JBIServiceDAL.BindingModel;
using JBIServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceDAL.Interfaces
{
    public interface ISostavService
    {
        List<SostavViewModel> GetList();

        SostavViewModel GetElement(int id);

        void AddElement(SostavBindingModel model);

        void UpdElement(SostavBindingModel model);

        void DelElement(int id);
    }
}
