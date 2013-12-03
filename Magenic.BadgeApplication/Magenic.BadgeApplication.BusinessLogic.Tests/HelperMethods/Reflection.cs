using System;
using System.Linq;
using System.Reflection;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.HelperMethods
{
    internal class Reflection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static object ExecuteMethod(Type type, string methodName, object instance, object[] methodParams, BindingFlags bindingFlags)
        {
            MethodInfo targetMethod = null;
            var methods = type.GetMethods(bindingFlags);

            var namedMethods = methods.Where(_ => _.Name == methodName && _.GetParameters().Count() == methodParams.Count());
            foreach (var namedMethod in namedMethods)
            {
                var found = true;
                for (var i = 0; i < namedMethod.GetParameters().Count(); i++)
                {
                    if (namedMethod.GetParameters()[i].GetType().IsInstanceOfType(methodParams[i]))
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    targetMethod = namedMethod;
                }
            }

            return targetMethod == null ? null : targetMethod.Invoke(instance, methodParams);

        }
    }
}
