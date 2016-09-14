using Microsoft.Practices.Unity;
using System;

namespace EoraMarketplace.Injector
{
    public abstract class Bootstrapper<T> where T : Bootstrapper<T>
    {
        private static Bootstrapper<T> _instance;
        public static Bootstrapper<T> Instance
        {
            get {
                if(_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                }
                return _instance;
            }
        }

        public static IUnityContainer SetupDependency(IUnityContainer container)
        {
            Instance.RegisterTypes(container);
            return container;
        }

        internal protected abstract void RegisterTypes(IUnityContainer container);
    }
}
