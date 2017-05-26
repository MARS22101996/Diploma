using System.Collections.Generic;
using Periodicals.BLL.Dto;

namespace Periodicals.ViewModels
{
    public class UserStatisticViewModel
    {
        public IEnumerable<UserPublicationDto> Subscribes { get; set; }

        public double Sum { get; set; }
    }
}