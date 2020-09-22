using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Netension.Monitoring.Core.Diagnostics
{
    /// <summary>
    /// Collection of the stopwatches. Makes easire to handle multiple stopwatches.
    /// </summary>
    public class StopwatchCollection
    {
        private readonly ILogger<StopwatchCollection> _logger;

        private readonly IDictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();

        public StopwatchCollection(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<StopwatchCollection>();
        }

        /// <summary>
        /// Stop stopwatch and get elapsed time.
        /// </summary>
        /// <param name="key">Key of the stopwatch.</param>
        /// <returns>Elapsed time. If the stopwatch does not exist with given key the result will be TimeSpan.Zero.</returns>
        public TimeSpan this[string key]
        {
            get { return Stop(key)?.Elapsed ?? TimeSpan.Zero; }
        }

        /// <summary>
        /// Start a new stopwatch. The existing stopwatch will not be replaced.
        /// </summary>
        /// <param name="key">Key of the new stopwatch.</param>
        public void Start(string key)
        {
            _logger.LogDebug("Start {key} stopwatch.", key);

            if (_stopwatches.ContainsKey(key))
            {
                _logger.LogWarning("{key} stopwatch is already started.", key);
                return;
            }

            _stopwatches.Add(key, Stopwatch.StartNew());
        }

        /// <summary>
        /// Stop the stopwatch.
        /// </summary>
        /// <param name="key">Key of the stopwatch.</param>
        /// <returns>Returns with stopwatch, or default(Stopwatch) it does not exist.</returns>
        public Stopwatch Stop(string key)
        {
            _logger.LogDebug("Stop {key} stopwatch.", key);
            if (!_stopwatches.ContainsKey(key))
            {
                _logger.LogWarning("{key} stopwatch does not exist.");
                return default;
            }

            var result = _stopwatches[key];
            result.Stop();
            _stopwatches.Remove(key);

            return result;
        }
    }
}
