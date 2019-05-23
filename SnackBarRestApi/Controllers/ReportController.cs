using SnackBarServiceDAL.BindingModel;
using SnackBarServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SnackBarRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetStocksLoad()
        {
            var list = _service.GetStocksLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetClientOrders(ReportBindingModel model)
        {
            var list = _service.GetClientOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
                
            }
            return Ok(list);
        }
        [HttpPost]
        public void SaveSnackPrice(ReportBindingModel model)
        {
            _service.SaveSnackPrice(model);
        }
        [HttpPost]
        public void SaveStocksLoad(ReportBindingModel model)
        {
            _service.SaveStocksLoad(model);
        }
        [HttpPost]
        public void SaveClientOrders(ReportBindingModel model)
        {
            _service.SaveClientOrders(model);
        }
    }
}
