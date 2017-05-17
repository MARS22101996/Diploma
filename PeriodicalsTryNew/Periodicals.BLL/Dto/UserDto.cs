using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.BLL.Dto
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobilePhone { get; set; }

        public string City { get; set; }

        public string Index { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImgMimeType { get; set; }
    }
}
