using System.IO;
using System.Linq;
using Castle.DynamicProxy;

namespace Interceptors.Interceptors
{
    public class CallLogger : IInterceptor
    {
        public readonly TextWriter Output;

        public CallLogger(TextWriter output)
        {
            Output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            Output.WriteLine("Calling method {0} with parameters {1}.",
              invocation.Method.Name,
              string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            invocation.Proceed();

            Output.WriteLine("Done: result was {0}.", invocation.ReturnValue);
        }
    }
}
