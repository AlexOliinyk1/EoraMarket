﻿using Microsoft.Practices.Unity;
using EoraMarketplace.DataAccess.Repositories;
using EoraMarketplace.Data.Domain.Users;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;

namespace EoraMarketplace.Injector.Services
{
    public class RepositoryBootstrapper : Bootstrapper<RepositoryBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Role>, Repository<Role>>();

            container.RegisterType<IRepository<Character>, Repository<Character>>();
            container.RegisterType<IRepository<Class>, Repository<Class>>();
            container.RegisterType<IRepository<Race>, Repository<Race>>();
            container.RegisterType<IRepository<CharactersProducts>, Repository<CharactersProducts>>();

            container.RegisterType<IRepository<MarketProduct>, Repository<MarketProduct>>();
        }
    }
}
