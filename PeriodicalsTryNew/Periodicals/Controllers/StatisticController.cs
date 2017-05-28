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
using Periodicals.Extention;

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
                StartYear = 2016,
                Month = 2
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetStatistic(int year, int month)
        {
            UserStatisticDto statistic;
            DateTime date;
            int day = 1;

            SetUpDate(year, month, ref day, out date, out statistic);

            TempData["year"] = year;
            TempData["month"] = month;

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
            UserStatisticDto statistic;
            DateTime date;
            int day = 1;

            SetUpDate(year, month, ref day, out date, out statistic);

            if (statistic != null)
            {
                var model = new UserStatisticViewModel
                {
                    Year = year,
                    Month = date.ToMonthName(),
                    MonthInt = month,
                    Subscribes = statistic.Subscribes,
                    Sum = statistic.Sum
                };

                return View("Report", model);
            }
            return View("NoData");
        }

        public ActionResult GeneratePdf(string data)
        {
            var values = data.Split(' ');
            int year, month;
            if(TempData["year"]!=null && TempData["month"]!=null)
            {
                year = int.Parse(TempData["year"].ToString());
                month = int.Parse(TempData["month"].ToString());
            }
            else
            {
                year = int.Parse(values[0]);
                month = int.Parse(values[1]);
            }
            ////int year = int.Parse(values[0]);
            ////int month = int.Parse(values[1]);
            //int year = int.Parse(TempData["year"].ToString());
            //int month = int.Parse(TempData["month"].ToString());
            return new Rotativa.ActionAsPdf("GetStatisticPdf", new { year, month });
        }

        public JsonResult GetColorData(string data)
        {
            var values = data.Split(' ');
            int year = int.Parse(values[0]);
            int month = int.Parse(values[1]);
            UserStatisticDto statistic;
            DateTime date;
            int day = 1;

            SetUpDate(year, month, ref day, out date, out statistic);

            var model = statistic.ColorCountDictionary.Select(x => new TypeData()
            {
                Color = x.Key,
                Count = x.Value
            }).ToList();

            return Json(new { Countries = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPriceData(string data)
        {
            var values = data.Split(' ');
            int year = int.Parse(values[0]);
            int month = int.Parse(values[1]);
            UserStatisticDto statistic;
            DateTime date;
            int day = 1;

            SetUpDate(year, month, ref day, out date, out statistic); 

            var model = statistic.PriceCountDictionary.Select(x => new PriceData()
            {
                Price = x.Key.ToString(),
                Count = x.Value
            }).ToList();

            return Json(new { Countries = model }, JsonRequestBehavior.AllowGet);
        }


        private void SetUpDate(int year, int month, ref int day, out DateTime date, out UserStatisticDto statistic)
        {
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
            date = new DateTime(year, month, day, 0, 0, 0);

            statistic = _statisticService.GetStatisticFiltered(_factory, date);

        }
    }
}