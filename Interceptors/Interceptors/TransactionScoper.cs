using System.Transactions;
using Castle.DynamicProxy;

namespace Interceptors.Interceptors
{
    public class TransactionScoper : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            using (var tr = new TransactionScope())
            {
                invocation.Proceed();
                tr.Complete();                
            }
        }
    }
}
