using SnackBarModel;
using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private BarDbContext context;
        public MainServiceDB(BarDbContext context)
        {
            this.context = context;
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                SnackId = rec.SnackId,
                ДатаВыполнения = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                ДатаЗаказа = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Статус = rec.Status.ToString(),
                Количество = rec.Count,
                Сумма = rec.Sum,
                ФИОЗаказчика = rec.Client.ClientFIO,
                НазваниеЗакуски = rec.Snack.SnackName
            })
            .ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                ClientId = model.ClientId,
                SnackId = model.SnackId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
        using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != OrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var productProducts = context.SnackProducts.Include(rec =>
                    rec.Product).Where(rec => rec.SnackId == element.SnackId);
                    // списываем
                    foreach (var productProduct in productProducts)
                    {
                        int countOnStocks = productProduct.Count * element.Count;
                        var stockProducts = context.StockProducts.Where(rec =>
                        rec.ProductId == productProduct.ProductId);
                        foreach (var stockProduct in stockProducts)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockProduct.Count >= countOnStocks)
                            {
                                stockProduct.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockProduct.Count;
                                stockProduct.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                           productProduct.Product.ProductName + " требуется " + productProduct.Count + ", не хватает " + countOnStocks);
                         }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = OrderStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
        {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = OrderStatus.Готов;
            context.SaveChanges();
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
        }
        public void PutProductOnStock(StockProductBindingModel model)
        {
            StockProduct element = context.StockProducts.FirstOrDefault(rec =>
           rec.StockId == model.StockId && rec.ProductId == model.ProductId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockProducts.Add(new StockProduct
                {
                    StockId = model.StockId,
                    ProductId = model.ProductId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
