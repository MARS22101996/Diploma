using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.BLL.Dto;
using Periodicals.DAL.Repository.Abstract;

namespace Periodicals.BLL.Interfaces
{
    public interface IStatisticService
    {
        UserStatisticDto GetStatisticFiltered(IRepositoryFactory factory,string userName, DateTime startDate);
    }
}
