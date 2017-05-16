using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.BLL.Dto;
using Periodicals.BLL.Interfaces;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;

namespace Periodicals.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private const int DefaultStatisticDaysNumber = 14;


        public UserStatisticDto GetStatisticFiltered(IRepositoryFactory factory,string name, DateTime date)
        {
            var publication = factory.PublicationRepository.Get().Where(x => x.NameOfPublication == name).First();

            var subscribes =
                factory.UserPublicationRepository.Get()
                    .Where(x => x.PublicationId == publication.PublicationId && x.StartDate >= date).ToList();

            if (!subscribes.Any())
            {
                return null;
            }

            var statisticDaysNumber = GetStatisticDaysNumber(date);
            date = date == default(DateTime) ? DateTime.UtcNow.AddDays(-statisticDaysNumber) : date;

            var result = new UserStatisticDto
            {
                Subscribes = subscribes,
                DateCountOfTicketsDictionary = CountTicketsPerEachDay(subscribes, date, statisticDaysNumber),
                ColorCountDictionary = GetColorCountDictionary(factory,subscribes),
                PriceCountDictionary = GetPriceCountDictionary(factory,subscribes)
            };

            return result;
        }

        private int GetStatisticDaysNumber(DateTime startDate)
        {
            return startDate == default(DateTime) ? DefaultStatisticDaysNumber : (DateTime.UtcNow - startDate).Days;
        }

        private Dictionary<double, int> GetPriceCountDictionary(IRepositoryFactory factory, IReadOnlyCollection<UserPublication> tickets)
        {
            var priceCountDictionary = new Dictionary<double, int>();

            var prices = factory.PublicationRepository.Get().OrderBy(x => x.PricePerMonth).Select(x => x.PricePerMonth).Distinct();

            foreach (var price in prices)
            {
                var statusCount = tickets.Count(ticketDto => ticketDto.Publication.PricePerMonth == price);
                priceCountDictionary.Add(price, statusCount);
            }

            return priceCountDictionary;
        }

        private Dictionary<string, int> GetColorCountDictionary(IRepositoryFactory factory,IReadOnlyCollection<UserPublication> tickets)
        {
            var statusCountDictionary = new Dictionary<string, int>();

            var colors = factory.PublicationRepository.Get().OrderBy(x => x.Color).Select(x => x.Color).Distinct();

            foreach (var color in colors)
            {
                var colorCount = tickets.Count(ticketDto => ticketDto.Publication.Color == color);
                statusCountDictionary.Add(color, colorCount);
            }

            return statusCountDictionary;
        }

        private Dictionary<DateTime, int> CountTicketsPerEachDay(
            IReadOnlyCollection<UserPublication> tickets,
            DateTime startDate,
            int daysInterval)
        {
            var ticketsPerEachDay = new Dictionary<DateTime, int>();

            for (var i = 1; i <= daysInterval; i++)
            {
                var date = startDate.AddDays(i);
                var ticketCount = tickets.Count(ticketDto => ticketDto.StartDate.Date == date.Date);

                ticketsPerEachDay.Add(date, ticketCount);
            }

            return ticketsPerEachDay;
        }
    }
}
