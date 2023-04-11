using System;
using System.Collections.Generic;

namespace Roguelike.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices s_instance;
        private readonly Dictionary<Type, IService> _services = new();

        public static AllServices Container => s_instance ??= new AllServices();

        public void RegisterSingle<TService>(TService instance) where TService : IService => 
            _services.Add(typeof(TService), instance);

        public TService Single<TService>() where TService : class, IService
        {
            if (_services.ContainsKey(typeof(TService)))
                return _services[typeof(TService)] as TService;
            else
                throw new ArgumentNullException($"Service {typeof(TService)} does not exist!");
        }
    }
}