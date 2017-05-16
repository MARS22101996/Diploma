using System;
using System.Collections.Generic;
using Periodicals.DAL.Entities;

namespace Periodicals.BLL.Dto
{
    public class UserStatisticDto
    {
        public IDictionary<DateTime, int> DateCountOfTicketsDictionary { get; set; }

        public IDictionary<double, int> PriceCountDictionary { get; set; }

        public IDictionary<string, int> ColorCountDictionary { get; set; }

        public IEnumerable<UserPublication> Subscribes { get; set; }
    }
}
