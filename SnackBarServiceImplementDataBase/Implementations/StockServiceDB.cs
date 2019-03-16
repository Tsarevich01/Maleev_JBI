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
    public class StockServiceDB : IStockService
    {
        private BarDbContext context;
        public StockServiceDB(BarDbContext context)
        {
            this.context = context;
        }
        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = context.Stocks.Select(rec => new
           StockViewModel
            {
                Id = rec.Id,
                НазваниеСклада = rec.StockName
            })
            .ToList();
            return result;
        }
        public StockViewModel GetElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockViewModel
                {
                    Id = element.Id,
                    НазваниеСклада = element.StockName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName ==
           model.StockName);
            if (element != null)
            {
                throw new Exception("Уже есть Склад с таким Названием");
            }
            context.Stocks.Add(new Stock
            {
                StockName = model.StockName
            });
            context.SaveChanges();
        }
        public void UpdElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName ==
           model.StockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockName = model.StockName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Stocks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
