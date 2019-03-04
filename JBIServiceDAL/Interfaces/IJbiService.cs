using JBIServiceDAL.BindingModel;
using JBIServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceDAL.Interfaces
{
    public interface IJbiService
    {
        List<JbiViewModel> GetList();

        JbiViewModel GetElement(int id);

        void AddElement(JbiBindingModel model);

        void UpdElement(JbiBindingModel model);

        void DelElement(int id);
    }
}
