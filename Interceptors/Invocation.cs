using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Interceptors
{
    public class Invocation : AbstractInvocation
    {
        private readonly object[] _arguments;

        public Invocation(object proxy, IInterceptor[] interceptors, MethodInfo proxiedMethod, object[] arguments) 
            : base(proxy, interceptors, proxiedMethod, arguments)
        {
            InvocationTarget = proxy;
            MethodInvocationTarget = proxiedMethod;
            _arguments = arguments;
        }

        protected override void InvokeMethodOnTarget()
        {
            ReturnValue = MethodInvocationTarget.Invoke(InvocationTarget, _arguments);
        }

        public override object InvocationTarget { get; }

        public override Type TargetType => InvocationTarget.GetType();

        public override MethodInfo MethodInvocationTarget { get; }
    }
}
