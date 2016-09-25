using Microsoft.Practices.Unity;
using System;

namespace EoraMarketplace.Injector
{
    /// <summary>
    ///     Base injector class for registration of dependency injections of Unity container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Bootstrapper<T> where T : Bootstrapper<T>
    {
        private static Bootstrapper<T> _instance;
        /// <summary>
        ///     Get instance of current injector
        /// </summary>
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

        /// <summary>
        ///     Setup all dependency for current injector instance
        /// </summary>
        /// <param name="container">Unity container instance</param>
        /// <returns></returns>
        public static IUnityContainer SetupDependency(IUnityContainer container)
        {
            Instance.RegisterTypes(container);
            return container;
        }

        /// <summary>
        ///     Register all dependencies for current injector
        /// </summary>
        /// <param name="container">Instance of Unity container</param>
        internal protected abstract void RegisterTypes(IUnityContainer container);
    }
}
