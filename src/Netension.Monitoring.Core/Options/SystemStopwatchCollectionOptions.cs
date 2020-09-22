using System;
using System.Diagnostics;

namespace Netension.Monitoring.Core.Options
{
    public class SystemStopwatchCollectionOptions
    {
        public Func<string, Stopwatch> StopFallback { get; set; }
    }
}
