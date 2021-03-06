﻿using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBarServiceDAL.Interfaces
{
    public interface ISnackService
    {
        List<SnackViewModel> GetList();

        SnackViewModel GetElement(int id);

        void AddElement(SnackBindingModel model);

        void UpdElement(SnackBindingModel model);

        void DelElement(int id);
    }
}
