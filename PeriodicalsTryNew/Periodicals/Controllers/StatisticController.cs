using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Periodicals.BLL.Dto;
using Periodicals.BLL.Interfaces;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.ViewModels;

namespace Periodicals.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        private readonly IRepositoryFactory _factory;

        public StatisticController(IStatisticService statisticService, IRepositoryFactory factory)
        {
            _statisticService = statisticService;
            _factory = factory;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {         
            var model = new DashboardViewModel
            {
                StartYear = 2016
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetStatistic(int year, int month)
        {
            UserStatisticDto statistic;
            int day = 1;
            switch (month)
            {
                case 1:
                    day = 31;
                    break;
                case 2:
                    {
                        if (year == 2016 || year == 2012) day = 28;
                        else day = 28;
                        break;
                    }
                case 3:
                    day = 31;
                    break;
                case 4:
                    day = 30;
                    break;
                case 5:
                    day = 31;
                    break;
                case 6:
                    day = 30;
                    break;
                case 7:
                    day = 31;
                    break;
                case 8:
                    day = 31;
                    break;
                case 9:
                    day = 30;
                    break;
                case 10:
                    day = 31;
                    break;
                case 11:
                    day = 30;
                    break;
                case 12:
                    day = 31;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            DateTime date = new DateTime(year, month, day, 0, 0, 0);
            try
            {
                statistic = _statisticService.GetStatisticFiltered(_factory, date);
            }
            catch (Exception e)
            {
                return Json(null);
            }

            var jsonSettings = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyy",
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            var modelJson = JsonConvert.SerializeObject(statistic, jsonSettings);

            return Json(modelJson, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ViewResult GetStatisticPdf(int year, int month)
        {
            int day = 1;
            switch (month)
            {
                case 1:
                    day = 31;
                    break;
                case 2:
                    {
                        if (year == 2016 || year == 2012) day = 28;
                        else day = 28;
                        break;
                    }
                case 3:
                    day = 31;
                    break;
                case 4:
                    day = 30;
                    break;
                case 5:
                    day = 31;
                    break;
                case 6:
                    day = 30;
                    break;
                case 7:
                    day = 31;
                    break;
                case 8:
                    day = 31;
                    break;
                case 9:
                    day = 30;
                    break;
                case 10:
                    day = 31;
                    break;
                case 11:
                    day = 30;
                    break;
                case 12:
                    day = 31;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            DateTime date = new DateTime(year, month, day, 0, 0, 0);
           
                var statistic = _statisticService.GetStatisticFiltered(_factory, date);

            var model = new UserStatisticViewModel
            {
                Subscribes = statistic.Subscribes,
                Sum = statistic.Sum
            };

            return View("Report", model);
        }

        public ActionResult GeneratePdf(int year, int month)
        {
            return new Rotativa.ActionAsPdf("GetStatisticPdf", new { year, month });
        }
    }
}