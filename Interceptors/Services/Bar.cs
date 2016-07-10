using System;

namespace Interceptors.Services
{
    public class Bar : IFoo
    {
        private static readonly Random Rnd = new Random();

        public virtual double GetRandomNumber() => Rnd.Next();
    }
}
