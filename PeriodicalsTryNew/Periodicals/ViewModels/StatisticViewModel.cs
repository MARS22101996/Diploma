using System;
using System.Collections.Generic;
using Periodicals.DAL.Entities;

namespace Periodicals.ViewModels
{
    public class StatisticViewModel
    {
        public IDictionary<DateTime, int> DateCountDictionary { get; set; }

        public IDictionary<double, int> PriceCountDictionary { get; set; }

        public IDictionary<string, int> ColorCountDictionary { get; set; }

        public IEnumerable<UserPublication> Subscribes { get; set; }
    }
}