using SnackBarModel;
using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;

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
            List<SnackViewModel> result = new List<SnackViewModel>();

            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                List<SnackProductViewModel> snackProducts = new List<SnackProductViewModel>();

                for (int j = 0; j < source.SnackProducts.Count; ++j)
                {
                    if (source.SnackProducts[j].SnackId == source.Snacks[i].Id)
                    {
                        string productName = string.Empty;
                        for (int k = 0; k < source.Products.Count; ++k)
                        {
                            if (source.SnackProducts[j].ProductId == source.Products[k].Id)
                            {
                                productName = source.Products[k].ProductName;
                                break;
                            }
                        }
                        snackProducts.Add(new SnackProductViewModel
                        {
                            Id = source.SnackProducts[j].Id,
                            SnackId = source.SnackProducts[j].SnackId,
                            ProductId = source.SnackProducts[j].ProductId,
                            НазваниеПродукта = productName,
                            Количество = source.SnackProducts[j].Count
                        });

                    }
                }

                result.Add(new SnackViewModel
                {
                    Id = source.Snacks[i].Id,
                    НазваниеЗакуски = source.Snacks[i].SnackName,
                    Цена = source.Snacks[i].Price,
                    SnackProducts = snackProducts
                });
            }
            return result;
        }

        public SnackViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<SnackProductViewModel> setDishes = new List<SnackProductViewModel>();
                for (int j = 0; j < source.SnackProducts.Count; ++j)
                {
                    if (source.SnackProducts[j].SnackId == source.Snacks[i].Id)
                    {
                        string SostavName = string.Empty;
                        for (int k = 0; k < source.Products.Count; ++k)
                        {
                            if (source.SnackProducts[j].ProductId == source.Products[k].Id)
                            {
                                SostavName = source.Products[k].ProductName;
                                break;
                            }
                        }
                        setDishes.Add(new SnackProductViewModel
                        {
                            Id = source.SnackProducts[j].Id,
                            SnackId = source.SnackProducts[j].SnackId,
                            ProductId = source.SnackProducts[j].ProductId,
                            НазваниеПродукта = SostavName,
                            Количество = source.SnackProducts[j].Count
                        });
                    }
                }
                if (source.Snacks[i].Id == id)
                {
                    return new SnackViewModel
                    {
                        Id = source.Snacks[i].Id,
                        НазваниеЗакуски = source.Snacks[i].SnackName,
                        Цена = source.Snacks[i].Price,
                        SnackProducts = setDishes
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SnackBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                if (source.Snacks[i].Id > maxId)
                {
                    maxId = source.Snacks[i].Id;
                }
                if (source.Snacks[i].SnackName == model.SnackName)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }
            source.Snacks.Add(new Snack
            {
                Id = maxId + 1,
                SnackName = model.SnackName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.SnackProducts.Count; ++i)
            {
                if (source.SnackProducts[i].Id > maxPCId)
                {
                    maxPCId = source.SnackProducts[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.SnackProduct.Count; ++i)
            {
                for (int j = 1; j < model.SnackProduct.Count; ++j)
                {
                    if (model.SnackProduct[i].ProductId ==
                    model.SnackProduct[j].ProductId)
                    {
                        model.SnackProduct[i].Count +=
                        model.SnackProduct[j].Count;
                        model.SnackProduct.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.SnackProduct.Count; ++i)
            {
                source.SnackProducts.Add(new SnackProduct
                {
                    Id = ++maxPCId,
                    SnackId = maxId + 1,
                    ProductId = model.SnackProduct[i].ProductId,
                    Count = model.SnackProduct[i].Count
                });
            }
        }

        public void UpdElement(SnackBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                if (source.Snacks[i].Id == model.Id)
                {
                    index = i;
                }

                if (source.Snacks[i].SnackName == model.SnackName && source.Snacks[i].Id != model.Id)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Snacks[index].SnackName = model.SnackName;
            source.Snacks[index].Price = model.Price;
            int maxPCId = 0;

            for (int i = 0; i < source.SnackProducts.Count; ++i)
            {
                if (source.SnackProducts[i].Id > maxPCId)
                {
                    maxPCId = source.SnackProducts[i].Id;
                }
            }

            for (int i = 0; i < source.SnackProducts.Count; ++i)
            {
                if (source.SnackProducts[i].SnackId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.SnackProduct.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.SnackProducts[i].Id == model.SnackProduct[j].Id)
                        {
                            source.SnackProducts[i].Count = model.SnackProduct[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.SnackProducts.RemoveAt(i--);
                    }
                }
            }

            for (int i = 0; i < model.SnackProduct.Count; ++i)
            {
                if (model.SnackProduct[i].Id == 0)
                {
                    for (int j = 0; j < source.SnackProducts.Count; ++j)
                    {
                        if (source.SnackProducts[j].SnackId == model.Id && source.SnackProducts[j].ProductId == model.SnackProduct[i].ProductId)
                        {
                            source.SnackProducts[j].Count += model.SnackProduct[i].Count;
                            model.SnackProduct[i].Id = source.SnackProducts[j].Id;
                            break;
                        }
                    }

                    if (model.SnackProduct[i].Id == 0)
                    {
                        source.SnackProducts.Add(new SnackProduct
                        {
                            Id = ++maxPCId,
                            SnackId = model.Id,
                            ProductId = model.SnackProduct[i].ProductId,
                            Count = model.SnackProduct[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.SnackProducts.Count; ++i)
            {
                if (source.SnackProducts[i].SnackId == id)
                {
                    source.SnackProducts.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Snacks.Count; ++i)
            {
                if (source.Snacks[i].Id == id)
                {
                    source.Snacks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

    }
}
