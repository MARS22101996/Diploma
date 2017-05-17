using System;
using System.Collections.Generic;
using System.Linq;
using Periodicals.BLL.Dto;
using Periodicals.BLL.Interfaces;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using AutoMapper;

namespace Periodicals.BLL.Services
{
    public class StatisticService : IStatisticService
    {

        public UserStatisticDto GetStatisticFiltered(IRepositoryFactory factory, DateTime date)
        {          
            var subscribes =
                factory.UserPublicationRepository.Get()
                    .Where(o => o.PaymentState == true && o.StartDate <= date && date <= o.EndDate).ToList();

            var subscribesDto = Mapper.Map<List<UserPublicationDto>>(subscribes);

            if (!subscribes.Any())
            {
                return null;
            }

            var result = new UserStatisticDto
            {
                Subscribes = subscribesDto,              
                ColorCountDictionary = GetColorCountDictionary(factory, subscribes),
                PriceCountDictionary = GetPriceCountDictionary(factory, subscribes),
                Sum = subscribesDto.Sum(x=>x.Publication.PricePerMonth)
            };

            return result;
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
    }
}
