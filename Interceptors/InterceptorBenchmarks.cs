using System;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Castle.DynamicProxy;
using Interceptors.Interceptors;
using Interceptors.Services;

namespace Interceptors
{
    // http://blog.andreloker.de/post/2009/02/13/Simple-AOP-introduction-to-AOP.aspx
    [Config(typeof(FastAndDirtyConfig))]
    public class InterceptorBenchmarks : IDisposable
    {
        public static readonly TextWriter TextWriter = File.AppendText("write-line.txt");

        public static readonly TextWriter InterceptorTextWriter = File.AppendText("benchmarks.txt");

        private static readonly object[] Arguments = {};

        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        public static readonly IFoo Foo = new Foo();

        public static readonly IFoo Bus = new Bus();

        private static readonly CallLogger CallLogger  = new CallLogger(InterceptorTextWriter);

        private static readonly ErrorHandler ErrorHandler = new ErrorHandler(InterceptorTextWriter);

        public static readonly IInvocation Invocation = new Invocation(Foo,
            new IInterceptor[] {CallLogger}, typeof(IFoo).GetMethod("GetRandomNumber"), new object[] {});

        public static readonly IFoo FooInterfaceProxy
            = Generator.CreateInterfaceProxyWithTargetInterface(Foo);

        public static readonly Foo FooClassProxy
            = Generator.CreateClassProxy<Foo>();

        public static readonly Bar BarClassProxy
            = Generator.CreateClassProxy<Bar>();

        public static readonly Bus BusClassProxy
            = Generator.CreateClassProxy<Bus>();

        public static readonly IFoo FooInterfaceProxyWithCallLogerInterceptor
            = Generator.CreateInterfaceProxyWithTarget(Foo, CallLogger);

        public static readonly Bar BarClassProxyWithCallLogerInterceptor
            = Generator.CreateClassProxy<Bar>(CallLogger);

        public static readonly IFoo BarClassProxyWith2Interceptors
            = Generator.CreateClassProxy<Bar>(CallLogger, ErrorHandler);

        public static readonly IFoo FooInterfaceProxyWith2Interceptors
            = Generator.CreateInterfaceProxyWithTarget(Foo, CallLogger, ErrorHandler);

        public static readonly IFoo BusInterfaceProxyWith2Interceptors
            = Generator.CreateInterfaceProxyWithTarget(Bus, CallLogger, ErrorHandler);

        [Benchmark]
        public void CreateInstance()
        {
            var foo = new Foo();
        }

        [Benchmark]
        public void CreateClassProxy()
        {
             Generator.CreateClassProxy<Foo>();
        }

        [Benchmark]
        public void CreateClassProxyWithTarget()
        {
            Generator.CreateClassProxyWithTarget(new Foo());
        }

        [Benchmark]
        public void CreateInterfaceProxyWithTarget()
        {
            Generator.CreateInterfaceProxyWithTarget<IFoo>(new Foo());
        }

        [Benchmark]
        public void CreateInterfaceProxyWithoutTarget()
        {
            Generator.CreateInterfaceProxyWithoutTarget<IFoo>();
        }

        [Benchmark]
        public void Foo_GetRandomNumber()
        {
            Foo.GetRandomNumber();
        }

        [Benchmark]
        public void Foo_InterfaceProxyGetRandomNumber()
        {
            FooInterfaceProxy.GetRandomNumber();
        }

        [Benchmark]
        public void FooClassProxy_GetRandomNumber()
        {
            FooClassProxy.GetRandomNumber();
        }

        [Benchmark]
        public void BarClassProxy_GetRandomNumber()
        {
            BarClassProxy.GetRandomNumber();
        }

        [Benchmark]
        public void FooInterfaceProxyWithCallLoggerInterceptor_GetRandomNumber()
        {
            FooInterfaceProxyWithCallLogerInterceptor.GetRandomNumber();
        }

        [Benchmark]
        public void BarClassProxyWithCallLoggerInterceptor_GetRandomNumber()
        {
            BarClassProxyWithCallLogerInterceptor.GetRandomNumber();
        }

        [Benchmark]
        public void FooInterfaceProxyWith2Interceptors_GetRandomNumber()
        {
            FooInterfaceProxyWith2Interceptors.GetRandomNumber();
        }

        [Benchmark]
        public void  BarClassProxyWith2Interceptors_GetRandomNumber()
        {
            BarClassProxyWith2Interceptors.GetRandomNumber();
        }

        [Benchmark]
        public void Bus_GetRandomNumber()
        {
            Bus.GetRandomNumber();
        }

        [Benchmark]
        public void BusInterfaceProxyWith2Interceptors_GetRandomNumber()
        {
            BusInterfaceProxyWith2Interceptors.GetRandomNumber();
        }        

        [Benchmark]
        public void CallLogger_Intercept()
        {
            CallLogger.Intercept(Invocation);
        }

        [Benchmark]
        public void WriteLine()
        {
            TextWriter.WriteLine("Calling method {0} with parameters {1}.",
              "Benchmark",
              string.Join(", ", Arguments.Select(a => (a ?? "").ToString()).ToArray()));


            TextWriter.WriteLine("Done: result was {0}.", "Benchmark");
        }

        public void Dispose()
        {
            TextWriter.Dispose();
            InterceptorTextWriter.Dispose();
        }
    }
}
