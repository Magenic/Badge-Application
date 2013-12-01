using Autofac;
using Csla.Server;
using System;

namespace Magenic.BadgeApplication.BusinessLogic
{
    [Serializable]
    public sealed class ObjectActivator : IDataPortalActivator
    {
        public object CreateInstance(Type requestedType)
        {
            if (requestedType == null)
            {
                throw new ArgumentNullException("requestedType");
            }

            return requestedType.IsInterface ? IoC.Container.Resolve(requestedType) : Activator.CreateInstance(requestedType);
        }

        public void InitializeInstance(object obj)
        {
        }
    }
}