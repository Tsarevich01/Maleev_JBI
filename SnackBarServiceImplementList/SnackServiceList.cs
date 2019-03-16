using SnackBarModel;
using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackBarServiceImplementList
{
    public class SnackServiceList : ISnackService
    {
        private DataListSingleton source;

        public SnackServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<SnackViewModel> GetList()
        {
            List<SnackViewModel> result = source.Snacks
            .Select(rec => new SnackViewModel
            {
                Id = rec.Id,
                НазваниеЗакуски = rec.SnackName,
                Цена = rec.Price,
                SnackProducts = source.SnackProducts
            .Where(recPC => recPC.SnackId == rec.Id)
           .Select(recPC => new SnackProductViewModel
           {
               Id = recPC.Id,
               SnackId = recPC.SnackId,
               ProductId = recPC.ProductId,
               НазваниеПродукта = source.Products.FirstOrDefault(recC =>
                 recC.Id == recPC.ProductId)?.ProductName,
               Количество = recPC.Count
           })
           .ToList()
            })
            .ToList();
            
            return result;
        }

        public SnackViewModel GetElement(int id)
        {
            Snack element = source.Snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SnackViewModel
                {
                    Id = element.Id,
                    НазваниеЗакуски = element.SnackName,
                    Цена = element.Price,
                    SnackProducts = source.SnackProducts
                .Where(recPC => recPC.SnackId == element.Id)
                .Select(recPC => new SnackProductViewModel
                {
                    Id = recPC.Id,
                    SnackId = recPC.SnackId,
                    ProductId = recPC.ProductId,
                    НазваниеПродукта = source.Products.FirstOrDefault(recC =>
     recC.Id == recPC.ProductId)?.ProductName,
                    Количество = recPC.Count
                })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SnackBindingModel model)
        {
            Snack element = source.Snacks.FirstOrDefault(rec => rec.SnackName ==
           model.SnackName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Snacks.Count > 0 ? source.Snacks.Max(rec => rec.Id) :
           0;
            source.Snacks.Add(new Snack
            {
                Id = maxId + 1,
                SnackName = model.SnackName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.SnackProducts.Count > 0 ?
           source.SnackProducts.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupProducts = model.SnackProduct
            .GroupBy(rec => rec.ProductId)
           .Select(rec => new
           {
               ProductId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupProduct in groupProducts)
            {
                source.SnackProducts.Add(new SnackProduct
                {
                    Id = ++maxPCId,
                    SnackId = maxId + 1,

                    ProductId = groupProduct.ProductId,
                    Count = groupProduct.Count
                });
            }
        }
        public void UpdElement(SnackBindingModel model)
        {
            Snack element = source.Snacks.FirstOrDefault(rec => rec.SnackName ==
                model.SnackName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Snacks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SnackName = model.SnackName;
            element.Price = model.Price;
            int maxPCId = source.SnackProducts.Count > 0 ?
           source.SnackProducts.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.SnackProduct.Select(rec =>
           rec.ProductId).Distinct();
            var updateProducts = source.SnackProducts.Where(rec => rec.SnackId ==
           model.Id && compIds.Contains(rec.ProductId));
            foreach (var updateProduct in updateProducts)
            {
                updateProduct.Count = model.SnackProduct.FirstOrDefault(rec =>
               rec.Id == updateProduct.Id).Count;
            }
            source.SnackProducts.RemoveAll(rec => rec.SnackId == model.Id &&
           !compIds.Contains(rec.ProductId));
            // новые записи
            var groupProducts = model.SnackProduct
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.ProductId)
           .Select(rec => new
           {
               ProductId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupProduct in groupProducts)
            {
                SnackProduct elementPC = source.SnackProducts.FirstOrDefault(rec
               => rec.SnackId == model.Id && rec.ProductId == groupProduct.ProductId);
                if (elementPC != null)
                {
                    elementPC.Count += groupProduct.Count;
                }
                else
                {
                    source.SnackProducts.Add(new SnackProduct
                    {
                        Id = ++maxPCId,
                        SnackId = model.Id,
                        ProductId = groupProduct.ProductId,
                        Count = groupProduct.Count
                    });
                }
            }           
        }
        public void DelElement(int id)
        {
            Snack element = source.Snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.SnackProducts.RemoveAll(rec => rec.SnackId == id);
                source.Snacks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
