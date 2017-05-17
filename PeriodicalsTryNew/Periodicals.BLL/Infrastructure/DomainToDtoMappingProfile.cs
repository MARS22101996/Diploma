using AutoMapper;
using Periodicals.BLL.Dto;
using Periodicals.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.BLL.Infrastructure
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Publication, PublicationDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<UserPublication, UserPublicationDto>().ReverseMap();
        }
    }
}
