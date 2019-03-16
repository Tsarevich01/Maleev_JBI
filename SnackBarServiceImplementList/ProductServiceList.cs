using SnackBarModel;
using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceImplementList
{
    public class ProductServiceList : IProductService
    {
        private DataListSingleton source;

        public ProductServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddElement(ProductBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id > maxId)
                {
                    maxId = source.Products[i].Id;
                }
                if (source.Products[i].ProductName == model.ProductName)
                {
                    throw new Exception("Уже есть такой ингредиент");
                }
            }
            source.Products.Add(new Product
            {
                Id = maxId + 1,
                ProductName = model.ProductName
            });
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public ProductViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == id)
                {
                    return new ProductViewModel
                    {
                        Id = source.Products[i].Id,
                        НазваниеПродукта = source.Products[i].ProductName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<ProductViewModel> GetList()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            for (int i = 0; i < source.Products.Count; ++i)
            {
                result.Add(new ProductViewModel
                {
                    Id = source.Products[i].Id,
                    НазваниеПродукта = source.Products[i].ProductName
                });
            }

            return result;
        }

        public void UpdElement(ProductBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Products[i].ProductName == model.ProductName &&
                source.Products[i].Id != model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Products[index].ProductName = model.ProductName;
        }
    }
}
