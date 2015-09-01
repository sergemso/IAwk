using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using IAwk.IocContainer;

namespace IAwk.Ioc.WebApi
{
    public class IocDependencyResolver : IDependencyResolver
    {
        private readonly IIocContainer _container;

        public IocDependencyResolver(IIocContainer container)
        {
            _container = container;
        }

        public Object GetService(Type serviceType)
        {
            Object result;

            try
            {
                if (_container.IsRegistered(serviceType))
                {
                    result = _container.Resolve(serviceType);
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            Object service;

            service = GetService(serviceType);

            return new[] {service};
        }

        public IDependencyScope BeginScope()
        {
            return new IocDependencyResolver(_container.CreateScope());
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}