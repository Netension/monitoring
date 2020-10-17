using Microsoft.AspNetCore.Mvc;
using Netension.Monitoring.Prometheus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netension.Monitoring.Example.NetCoreWebApplication.Controllers
{
    [Route("")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ICounterManager _counterManager;
        private readonly IGaugeManager _gaugeManager;
        private readonly IHistogramManager _histogramManager;
        private readonly ISummaryManager _summaryManager;

        public ExampleController(ICounterManager counterManager, IGaugeManager gaugeManager, IHistogramManager histogramManager, ISummaryManager summaryManager)
        {
            _counterManager = counterManager;
            _gaugeManager = gaugeManager;
            _histogramManager = histogramManager;
            _summaryManager = summaryManager;
        }

        [HttpGet]
        [Route("counter-increase")]
        public IActionResult CounterIncrease()
        {
            _counterManager["example_counter_metric"].Inc();
            _counterManager.Increase("example_counter_metric_without_label");
            _counterManager.Increase("example_counter_metric_with_label", 2.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("counter-set")]
        public IActionResult CounterSet()
        {
            _counterManager["example_counter_metric"].IncTo(5.0);
            _counterManager.Set("example_counter_metric", 5.0);
            _counterManager.Set("example_counter_metric_with_label", 5.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("gauge-increase")]
        public IActionResult GaugeIncrease()
        {
            _gaugeManager["example_gauge_metric"].Inc();
            _gaugeManager.Increase("example_gauge_metric_without_label");
            _gaugeManager.Increase("example_gauge_metric_with_label", 2.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("gauge-decrease")]
        public IActionResult GaugeDecrease()
        {
            _gaugeManager["example_gauge_metric"].Dec();
            _gaugeManager.Decrease("example_gauge_metric_without_label");
            _gaugeManager.Decrease("example_gauge_metric_with_label", 2.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("gauge-set")]
        public IActionResult GaugeSet()
        {
            _gaugeManager.Set("example_gauge_metric", 5.0);
            _gaugeManager.Set("example_gauge_metric_with_label", 5.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("histogram_observe")]
        public IActionResult HistogramObserve()
        {
            _histogramManager["example_histogram_metric"].Observe(2.0);
            _histogramManager.Observe("example_histogram_metric_without_label", 2.0);
            _histogramManager.Observe("example_histogram_metric_with_label", 2.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("summary_observe")]
        public IActionResult SummaryObserve()
        {
            _summaryManager["example_summary_metric"].Observe(2.0);
            _summaryManager.Observe("example_summary_metric_without_label", 2.0);
            _summaryManager.Observe("example_summary_metric_with_label", 2.0, "label1", "label2");
            return Ok();
        }

        [HttpGet]
        [Route("summary_timing")]
        public async Task<IActionResult> SummaryTiming()
        {
            using (var stopwatch = _summaryManager.MeasureDuration("example_summary_metric_without_label"))
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
            }

            using (var stopwatch = _summaryManager.MeasureDuration("example_summary_metric_with_label", "label1", "label2"))
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            return Ok();
        }
    }
}
