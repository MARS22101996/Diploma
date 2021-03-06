﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;
using Periodicals.BLL.Interfaces;
using Periodicals.BLL.Services;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.DAL.Repository.Concrete;
using AutoMapper;

namespace Periodicals.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IRepositoryFactory>().To<EFRepositoryFactory>().WithConstructorArgument("ApplicationDbContext");
            _kernel.Bind<IStatisticService>().To<StatisticService>();            
        }
    }
}