using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
            const int defaultStatisticDays = 14;

            var model = new DashboardViewModel
            {
                StartDate = DateTime.UtcNow.AddDays(-defaultStatisticDays)
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetStatistic(string userName, DateTime startDate)
        {
            UserStatisticDto statistic;

            try
            {
                statistic = _statisticService.GetStatisticFiltered(_factory,userName, startDate);
            }
            catch (Exception)
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
        public JsonResult AutocompleteSearch(string term)
        {
            var model = _factory.PublicationRepository.Get().
                Where(a => a.NameOfPublication.ToLower().
                Contains(term.ToLower())).
                Select(a => a.NameOfPublication).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}