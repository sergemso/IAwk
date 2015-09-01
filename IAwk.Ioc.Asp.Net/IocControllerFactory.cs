using System;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using IAwk.IocContainer;

namespace IAwk.Ioc.Asp.Net
{
    public class IocControllerFactory : DefaultControllerFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<IocControllerFactory>();
        private readonly IIocContainer _container;

        public IocControllerFactory(IIocContainer container)
        {
            _container = container;
        }

        public override IController CreateController(RequestContext requestContext, String controllerName)
        {
            IController result;

            result = null;

            try
            {
                var type = GetControllerType(requestContext, controllerName);

                if (type != null && _container.IsRegistered(type))
                {
                    result = (IController) _container.Resolve(type);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            if (result == null)
            {
                result = base.CreateController(requestContext, controllerName);
            }

            return result;
        }
    }
}