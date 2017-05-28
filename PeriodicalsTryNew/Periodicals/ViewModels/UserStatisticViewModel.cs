using System.Collections.Generic;
using Periodicals.BLL.Dto;

namespace Periodicals.ViewModels
{
    public class UserStatisticViewModel
    {
        public int Year { get; set; }

        public string Month { get; set; }

        public int MonthInt { get; set; }

        public IEnumerable<UserPublicationDto> Subscribes { get; set; }

        public double Sum { get; set; }
    }
}