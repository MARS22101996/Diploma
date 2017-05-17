using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.BLL.Dto
{
    public class UserPublicationDto
    {
        public UserPublicationDto()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        public int UserPublicationId { get; set; }
        public int PublicationId { get; set; }
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Period { get; set; }
        public bool PaymentState { get; set; }

        public  UserDto User { get; set; }
        public  PublicationDto Publication { get; set; }
    }
}
