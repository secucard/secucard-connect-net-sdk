namespace Secucard.Connect.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Secucard.Connect.Product.Common.Model;

    internal class ServiceFactory
    {
        public static Dictionary<string, IService> CreateServices(ClientContext context)
        {
            Dictionary<string, IService> dic = new Dictionary<string, IService>();


            foreach (Type type in
                Assembly.GetAssembly(typeof(ProductService<SecuObject>)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IService).IsAssignableFrom(myType)))
            {
                IService service = (IService)Activator.CreateInstance(type);
                service.Context = context;
                service.RegisterEvents();
                dic.Add(service.GetType().Name, service);
            }

            return dic;
        }
    }
}
