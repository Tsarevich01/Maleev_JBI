using SnackBarModel;
using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceImplementDataBase.Implementations
{
    public class SnackServiceDB : ISnackService
    {
        private BarDbContext context;
        public SnackServiceDB(BarDbContext context)
        {
            this.context = context;
        }
        public List<SnackViewModel> GetList()
        {
            List<SnackViewModel> result = context.Snacks.Select(rec => new
           SnackViewModel
            {
                Id = rec.Id,
                НазваниеЗакуски = rec.SnackName,
                Цена = rec.Price,
                SnackProducts = context.SnackProducts
            .Where(recPC => recPC.SnackId == rec.Id)
           .Select(recPC => new SnackProductViewModel
           {
               Id = recPC.Id,
               SnackId = recPC.SnackId,
               ProductId = recPC.ProductId,
               НазваниеПродукта = recPC.Product.ProductName,
               Количество = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public SnackViewModel GetElement(int id)
        {
            Snack element = context.Snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SnackViewModel
                {
                    Id = element.Id,
                    НазваниеЗакуски = element.SnackName,
                    Цена = element.Price,
                    SnackProducts = context.SnackProducts
 .Where(recPC => recPC.SnackId == element.Id)
 .Select(recPC => new SnackProductViewModel
 {
     Id = recPC.Id,
     SnackId = recPC.SnackId,
     ProductId = recPC.ProductId,
     НазваниеПродукта = recPC.Product.ProductName,
     Количество = recPC.Count
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SnackBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.Snacks.FirstOrDefault(rec =>
                   rec.SnackName == model.SnackName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Snack
                    {
                        SnackName = model.SnackName,
                        Price = model.Price
                    };
                    context.Snacks.Add(element);
                    context.SaveChanges();
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
                        context.SnackProducts.Add(new SnackProduct
                        {
                            SnackId = element.Id,
                            ProductId = groupProduct.ProductId,
                            Count = groupProduct.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(SnackBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.Snacks.FirstOrDefault(rec =>
                   rec.SnackName == model.SnackName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Snacks.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SnackName = model.SnackName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.SnackProduct.Select(rec =>
                   rec.ProductId).Distinct();
                    var updateProducts = context.SnackProducts.Where(rec =>
                   rec.SnackId == model.Id && compIds.Contains(rec.ProductId));
                    foreach (var updateProduct in updateProducts)
                    {
                        updateProduct.Count =
                       model.SnackProduct.FirstOrDefault(rec => rec.Id == updateProduct.Id).Count;
                    }
                    context.SaveChanges();
                    context.SnackProducts.RemoveRange(context.SnackProducts.Where(rec =>
                    rec.SnackId == model.Id && !compIds.Contains(rec.ProductId)));
                    context.SaveChanges();
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
                        SnackProduct elementPC =
                       context.SnackProducts.FirstOrDefault(rec => rec.SnackId == model.Id &&
                       rec.ProductId == groupProduct.ProductId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupProduct.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.SnackProducts.Add(new SnackProduct
                            {
                                SnackId = model.Id,
                                ProductId = groupProduct.ProductId,
                                Count = groupProduct.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.Snacks.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.SnackProducts.RemoveRange(context.SnackProducts.Where(rec =>
                        rec.SnackId == id));
                        context.Snacks.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

