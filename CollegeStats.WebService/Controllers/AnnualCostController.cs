using CollegeStats.BusinessLogic.Interface;
using System;
using System.Web.Mvc;
using CollegeStats.WebService.Models;
using Autofac;

namespace CollegeStats.WebService.Controllers
{
    public class AnnualCostController : Controller
    {
        private IAnnualCostService _service;
        public AnnualCostController()
        {
            AutoFacMapper.BeginLifeTimeScope();
            _service = AutoFacMapper.LifeTimeScope.Resolve<IAnnualCostService>();
            AutoFacMapper.EndLifeTimeScope();
        }
        [HttpGet]
        // GET: AnnualCost
        // Ex: http://localhost:61567/AnnualCost/Get?collegeName=Gordon%20College
        public JsonResult Get(string collegeName, bool includeRoom = true, bool isOutOfState = false)
        {
            var jsonResponse = new JsonResponseModel();
            try
            {
                var data = _service.GetAnnualCost(collegeName, includeRoom, isOutOfState);
                jsonResponse.Data = data;
                jsonResponse.Status = Status.Ok;
                return Json(jsonResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonResponse.Status = Status.Error;
                jsonResponse.Message = e.Message;
                return Json(jsonResponse, JsonRequestBehavior.AllowGet);
            }
        }
    }
}