namespace Secucard.Connect.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Secucard.Connect.Product.Common.Model;

    internal static class ServiceFactory
    {
        /// <summary>
        /// Create all services thru reflection at startup
        /// </summary>
        public static Dictionary<string, IService> CreateServices(ClientContext context)
        {
            Dictionary<string, IService> dic = new Dictionary<string, IService>();


            foreach (var type in
                Assembly.GetAssembly(typeof (ProductService<SecuObject>)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && typeof (IService).IsAssignableFrom(myType))
                )
            {
                var service = (IService) Activator.CreateInstance(type);
                service.Context = context;
                service.RegisterEvents();
                dic.Add(service.GetType().Name, service);
            }

            return dic;
        }
    }
}