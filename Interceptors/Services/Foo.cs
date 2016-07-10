using System;

namespace Interceptors.Services
{
    public class Foo : IFoo
    {
        private static readonly Random Rnd = new Random();

        public double GetRandomNumber() => Rnd.Next();
    }
}
