using AutoMapper;
using Periodicals.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Periodicals.App_Start
{
    public class AutoMapperConfiguration
    {
        //public MapperConfiguration Configure()
        //{
        //    var mapperConfiguration = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile<DomainToDtoMappingProfile>();

        //        Mapper.Initialize(cfg);
        //    });

        //    return mapperConfiguration;
        //}
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDtoMappingProfile>();
            });
        }
    }
}