using JBIModel;
using JBIServiceDAL.BindingModel;
using JBIServiceDAL.Interfaces;
using JBIServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBIServiceImplementList
{
    public class SostavServiceList : ISostavService
    {
        private DataListSingleton source;

        public SostavServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(SostavBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Sostavs.Count; ++i)
            {
                if (source.Sostavs[i].Id > maxId)
                {
                    maxId = source.Sostavs[i].Id;
                }
                if (source.Sostavs[i].SostavName == model.SostavName)
                {
                    throw new Exception("Уже есть такой ингредиент");
                }
            }
            source.Sostavs.Add(new Sostav
            {
                Id = maxId + 1,
                SostavName = model.SostavName
            });
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Sostavs.Count; ++i)
            {
                if (source.Sostavs[i].Id == id)
                {
                    source.Sostavs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public SostavViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sostavs.Count; ++i)
            {
                if (source.Sostavs[i].Id == id)
                {
                    return new SostavViewModel
                    {
                        Id = source.Sostavs[i].Id,
                        SostavName = source.Sostavs[i].SostavName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<SostavViewModel> GetList()
        {
            List<SostavViewModel> result = new List<SostavViewModel>();
            for (int i = 0; i < source.Sostavs.Count; ++i)
            {
                result.Add(new SostavViewModel
                {
                    Id = source.Sostavs[i].Id,
                    SostavName = source.Sostavs[i].SostavName
                });
            }

            return result;
        }

        public void UpdElement(SostavBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sostavs.Count; ++i)
            {
                if (source.Sostavs[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Sostavs[i].SostavName == model.SostavName &&
                source.Sostavs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sostavs[index].SostavName = model.SostavName;
        }
    }
}
