using System.Diagnostics;

namespace Netension.Monitoring.Abstraction
{
    public interface IStopwatchCollection
    {
        void Start(string key);
        Stopwatch Stop(string key);
    }
}
