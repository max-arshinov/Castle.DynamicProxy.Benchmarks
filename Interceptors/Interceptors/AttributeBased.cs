using System;
using System.Linq;
using Castle.DynamicProxy;

namespace Interceptors.Interceptors
{
    public abstract class AttributeBased<T> : IInterceptor
        where T:Attribute
    {
        public void Intercept(IInvocation invocation)
        {
            var attrs = invocation.Method
                .GetCustomAttributes(typeof(T), true)
                .Cast<T>()
                .ToArray();

            if (!attrs.Any())
            {
                invocation.Proceed();
            }
            else
            {
                Intercept(invocation, attrs);
            }
        }

        protected abstract void Intercept(IInvocation invocation, params T[] attr);
    }
}
