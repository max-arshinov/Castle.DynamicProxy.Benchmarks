using System;
using System.IO;
using Castle.DynamicProxy;

namespace Interceptors.Interceptors
{
    public class ErrorHandler : IInterceptor
    {
        public readonly TextWriter Output;

        public ErrorHandler(TextWriter output)
        {
            Output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                Output.WriteLine($"Method {0} enters in try/catch block", invocation.Method.Name);
                invocation.Proceed();
                Output.WriteLine("End of try/catch block");
            }
            catch (Exception ex)
            {
                Output.WriteLine("Exception: " + ex.Message);
                throw new ValidationException("Sorry, Unhandaled exception occured", ex);
            }
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
