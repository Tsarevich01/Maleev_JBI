﻿using SnackBarModel;
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
            List<OrderViewModel> result = new List<OrderViewModel>();
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Clients.Count; ++j)
                {
                    if (source.Clients[j].Id == source.Orders[i].ClientId)
                    {
                        clientFIO = source.Clients[j].ClientFIO;
                        break;
                    }
                }
                string snackName = string.Empty;
                for (int j = 0; j < source.Snacks.Count; ++j)
                {
                    if (source.Snacks[j].Id == source.Orders[i].SnackId)
                    {
                        snackName = source.Snacks[j].SnackName;
                        break;
                    }
                }
                result.Add(new OrderViewModel
                {
                    Id = source.Orders[i].Id,
                    ClientId = source.Orders[i].ClientId,
                    ФИОЗаказчика = clientFIO,
                    SnackId = source.Orders[i].SnackId,
                    НазваниеЗакуски = snackName,
                    Количество = source.Orders[i].Count,
                    Сумма = source.Orders[i].Sum,
                    ДатаЗаказа = source.Orders[i].DateCreate.ToLongDateString(),
                    ДатаВыполнения = source.Orders[i].DateImplement?.ToLongDateString(),
                    Статус = source.Orders[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id > maxId)
                {
                    maxId = source.Clients[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Orders[index].Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Orders[index].DateImplement = DateTime.Now;
            source.Orders[index].Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(OrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            if (source.Orders[index].Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Orders[index].Status = OrderStatus.Готов;
        }

        public void PayOrder(OrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Orders[index].Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Orders[index].Status = OrderStatus.Оплачен;
        }
    }
}
