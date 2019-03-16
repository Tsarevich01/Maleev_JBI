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
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                SnackId = rec.SnackId,
                ДатаСоздания = rec.DateCreate.ToLongDateString(),
                ДатаЗавершения = rec.DateImplement?.ToLongDateString(),
                Статус = rec.Status.ToString(),
                Количество = rec.Count,
                Сумма = rec.Sum,
                ФИОЗаказчика = source.Clients.FirstOrDefault(recC => recC.Id ==
     rec.ClientId)?.ClientFIO,
                НазваниеЗакуски = source.Products.FirstOrDefault(recP => recP.Id ==
    rec.SnackId)?.ProductName,
            })
            .ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                ClientId = model.ClientId,
                SnackId = model.SnackId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var snackProducts = source.SnackProducts.Where(rec => rec.SnackId
           == element.SnackId);
            foreach (var snackProduct in snackProducts)
            {
                int countOnStocks = source.StockProducts
                .Where(rec => rec.ProductId ==
               snackProduct.ProductId)
               .Sum(rec => rec.Count);
                if (countOnStocks < snackProduct.Count * element.Count)
                {
                    var productName = source.Products.FirstOrDefault(rec => rec.Id ==
                   snackProduct.ProductId);
                    throw new Exception("Не достаточно компонента " +
                   productName?.ProductName + " требуется " + (snackProduct.Count * element.Count) +
                   ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var snackProduct in snackProducts)
            {
                int countOnStocks = snackProduct.Count * element.Count;
                var stockProducts = source.StockProducts.Where(rec => rec.ProductId
               == snackProduct.ProductId);
                foreach (var stockProduct in stockProducts)
                {
                    // компонентов на одном слкаде может не хватать
                    if (stockProduct.Count >= countOnStocks)
                    {
                        stockProduct.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockProduct.Count;
                        stockProduct.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }
        public void FinishOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = OrderStatus.Готов;
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
        }        public void PutProductOnStock(StockProductBindingModel model)
        {
            StockProduct element = source.StockProducts.FirstOrDefault(rec =>
           rec.StockId == model.StockId && rec.ProductId == model.ProductId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StockProducts.Count > 0 ?
               source.StockProducts.Max(rec => rec.Id) : 0;
                source.StockProducts.Add(new StockProduct
                {
                    Id = ++maxId,
                    StockId = model.StockId,
                    ProductId = model.ProductId,
                    Count = model.Count
                });
            }
        }
    }
}
