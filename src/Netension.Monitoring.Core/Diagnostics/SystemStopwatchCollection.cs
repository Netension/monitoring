using Netension.Monitoring.Abstraction;
using Netension.Monitoring.Core.Options;
using System.Collections.Generic;
using System.Diagnostics;

namespace Netension.Monitoring.Core.Diagnostics
{
    internal class SystemStopwatchCollection : IStopwatchCollection
    {
        private readonly SystemStopwatchCollectionOptions _options;

        private readonly IDictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();

        public SystemStopwatchCollection()
            : this(new SystemStopwatchCollectionOptions { StopFallback = (key) => default })
        {

        }

        public SystemStopwatchCollection(SystemStopwatchCollectionOptions options)
        {
            _options = options;
        }

        public void Start(string key)
        {
            _stopwatches.Add(key, Stopwatch.StartNew());
        }

        public Stopwatch Stop(string key)
        {
            if (!_stopwatches.ContainsKey(key)) return _options.StopFallback(key);

            var result = _stopwatches[key];
            result.Stop();
            _stopwatches.Remove(key);

            return result;
        }
    }
}
