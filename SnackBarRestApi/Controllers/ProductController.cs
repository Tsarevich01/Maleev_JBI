﻿using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SnackBarRestApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(ProductBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(ProductBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(ProductBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
