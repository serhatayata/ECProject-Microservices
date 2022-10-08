using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Enums;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes =
                type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            //Dışlanacak Methodlar. Bu method isimleri çakışmaya sebep olabiliyor.
            string[] methodsToExclude = { "Validate", "Info", "Debug", "Warning", "Warn", "Error", "Fatal", "Localize", "GetStringResource", "GetResourceValue" };

            if (!methodsToExclude.Contains(method.Name))
            {
                try//Api tarafında çağırılan bazı methodlar hataya sebep olduğu için try-catch bloğu içine aldım.
                {
                    var methodAttributes =
                        type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

                    classAttributes.AddRange(methodAttributes);
                }
                catch (Exception ex)
                {
                    // ignored 
                }
            }

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}