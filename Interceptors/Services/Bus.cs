using System.Threading;

namespace Interceptors.Services
{
    public class Bus : Bar
    {
        public override double GetRandomNumber()
        {
            Thread.Sleep(100);
            return base.GetRandomNumber();
        }
    }
}
