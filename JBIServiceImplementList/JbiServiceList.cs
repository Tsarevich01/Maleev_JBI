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
    public class JbiServiceList : IJbiService
    {
        private DataListSingleton source;

        public JbiServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<JbiViewModel> GetList()
        {
            List<JbiViewModel> result = new List<JbiViewModel>();

            for (int i = 0; i < source.Jbis.Count; ++i)
            {
                List<JbiSostavViewModel> setDishes = new List<JbiSostavViewModel>();

                for (int j = 0; j < source.JbiSostavs.Count; ++j)
                {
                    if (source.JbiSostavs[j].JbiId == source.Jbis[i].Id)
                    {
                        string dishName = string.Empty;
                        for (int k = 0; k < source.Sostavs.Count; ++k)
                        {
                            if (source.JbiSostavs[j].SostavId == source.Sostavs[k].Id)
                            {
                                dishName = source.Sostavs[k].SostavName;
                                break;
                            }
                        }
                        setDishes.Add(new JbiSostavViewModel
                        {
                            Id = source.JbiSostavs[j].Id,
                            JbiId = source.JbiSostavs[j].JbiId,
                            SostavId = source.JbiSostavs[j].SostavId,
                            SostavName = dishName,
                            Count = source.JbiSostavs[j].Count
                        });

                    }
                }

                result.Add(new JbiViewModel
                {
                    Id = source.Jbis[i].Id,
                    JbiName = source.Jbis[i].JbiName,
                    Price = source.Jbis[i].Price,
                    JbiSostavs = setDishes
                });
            }
            return result;
        }

        public JbiViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Jbis.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<JbiSostavViewModel> setDishes = new List<JbiSostavViewModel>();
                for (int j = 0; j < source.JbiSostavs.Count; ++j)
                {
                    if (source.JbiSostavs[j].JbiId == source.Jbis[i].Id)
                    {
                        string SostavName = string.Empty;
                        for (int k = 0; k < source.Sostavs.Count; ++k)
                        {
                            if (source.JbiSostavs[j].SostavId == source.Sostavs[k].Id)
                            {
                                SostavName = source.Sostavs[k].SostavName;
                                break;
                            }
                        }
                        setDishes.Add(new JbiSostavViewModel
                        {
                            Id = source.JbiSostavs[j].Id,
                            JbiId = source.JbiSostavs[j].JbiId,
                            SostavId = source.JbiSostavs[j].SostavId,
                            SostavName = SostavName,
                            Count = source.JbiSostavs[j].Count
                        });
                    }
                }
                if (source.Jbis[i].Id == id)
                {
                    return new JbiViewModel
                    {
                        Id = source.Jbis[i].Id,
                        JbiName = source.Jbis[i].JbiName,
                        Price = source.Jbis[i].Price,
                        JbiSostavs = setDishes
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(JbiBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Jbis.Count; ++i)
            {
                if (source.Jbis[i].Id > maxId)
                {
                    maxId = source.Jbis[i].Id;
                }
                if (source.Jbis[i].JbiName == model.JbiName)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }
            source.Jbis.Add(new Jbi
            {
                Id = maxId + 1,
                JbiName = model.JbiName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.JbiSostavs.Count; ++i)
            {
                if (source.JbiSostavs[i].Id > maxPCId)
                {
                    maxPCId = source.JbiSostavs[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.JbiSostav.Count; ++i)
            {
                for (int j = 1; j < model.JbiSostav.Count; ++j)
                {
                    if (model.JbiSostav[i].SostavId ==
                    model.JbiSostav[j].SostavId)
                    {
                        model.JbiSostav[i].Count +=
                        model.JbiSostav[j].Count;
                        model.JbiSostav.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.JbiSostav.Count; ++i)
            {
                source.JbiSostavs.Add(new JbiSostav
                {
                    Id = ++maxPCId,
                    JbiId = maxId + 1,
                    SostavId = model.JbiSostav[i].SostavId,
                    Count = model.JbiSostav[i].Count
                });
            }
        }

        public void UpdElement(JbiBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Jbis.Count; ++i)
            {
                if (source.Jbis[i].Id == model.Id)
                {
                    index = i;
                }

                if (source.Jbis[i].JbiName == model.JbiName && source.Jbis[i].Id != model.Id)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }

            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }

            source.Jbis[index].JbiName = model.JbiName;
            source.Jbis[index].Price = model.Price;
            int maxPCId = 0;

            for (int i = 0; i < source.JbiSostavs.Count; ++i)
            {
                if (source.JbiSostavs[i].Id > maxPCId)
                {
                    maxPCId = source.JbiSostavs[i].Id;
                }
            }

            for (int i = 0; i < source.JbiSostavs.Count; ++i)
            {
                if (source.JbiSostavs[i].JbiId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.JbiSostav.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.JbiSostavs[i].Id == model.JbiSostav[j].Id)
                        {
                            source.JbiSostavs[i].Count = model.JbiSostav[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.JbiSostavs.RemoveAt(i--);
                    }
                }
            }

            for (int i = 0; i < model.JbiSostav.Count; ++i)
            {
                if (model.JbiSostav[i].Id == 0)
                {
                    for (int j = 0; j < source.JbiSostavs.Count; ++j)
                    {
                        if (source.JbiSostavs[j].JbiId == model.Id && source.JbiSostavs[j].SostavId == model.JbiSostav[i].SostavId)
                        {
                            source.JbiSostavs[j].Count += model.JbiSostav[i].Count;
                            model.JbiSostav[i].Id = source.JbiSostavs[j].Id;
                            break;
                        }
                    }

                    if (model.JbiSostav[i].Id == 0)
                    {
                        source.JbiSostavs.Add(new JbiSostav
                        {
                            Id = ++maxPCId,
                            JbiId = model.Id,
                            SostavId = model.JbiSostav[i].SostavId,
                            Count = model.JbiSostav[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.JbiSostavs.Count; ++i)
            {
                if (source.JbiSostavs[i].JbiId == id)
                {
                    source.JbiSostavs.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Jbis.Count; ++i)
            {
                if (source.Jbis[i].Id == id)
                {
                    source.Jbis.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

    }
}
