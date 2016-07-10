using System;
using BenchmarkDotNet.Running;

namespace Interceptors
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<InterceptorBenchmarks>();
        }
    }
}
