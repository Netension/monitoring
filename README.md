![nuget-template](https://github.com/Netension/monitoring/blob/develop/banner.png)
__Netenson.Monitoring__ packages provide types that make easier the metrics integration.

A software metric is a measure of software characteristics which are quantifiable or countable. Software metrics are important for measuring software performance.

![Publish](https://github.com/Netension/monitoring/workflows/Publish/badge.svg)<br/>
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=alert_status)](https://sonarcloud.io/dashboard?id=Netension_monitoring)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=coverage)](https://sonarcloud.io/dashboard?id=Netension_monitoring)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=bugs)](https://sonarcloud.io/dashboard?id=Netension_monitoring)

# Packages
## [Netension.Monitoring.Core](https://www.nuget.org/packages/Netension.Monitoring.Core/) ![Nuget](https://img.shields.io/nuget/v/Netension.Monitoring.Core?label=NuGet&logo=NuGet&style=plastic)
Provides generally used types for metrics produce.

### StopwatchCollection
Supports to manage multiple stopwatches in an application.

---
## [Netension.Monitoring.Prometheus](https://www.nuget.org/packages/Netension.Monitoring.Prometheus/) ![Nuget](https://img.shields.io/nuget/v/Netension.Monitoring.Prometheus?label=NuGet&logo=NuGet&style=plastic)
Makes easier to use the [Prometheus](https://prometheus.io/) metrics.

[Prometheus](https://prometheus.io/) is an open-source systems monitoring and alerting toolkit.<br/>
[Prometheus](https://prometheus.io/)'s main features are:
- a multi-dimensional [data model](https://prometheus.io/docs/concepts/data_model/) with time series data identified by metric name and key/value pairs
- PromQL, a [flexible query language](https://prometheus.io/docs/prometheus/latest/querying/basics/) to leverage this dimensionality
- no reliance on distributed storage; single server nodes are autonomous
- time series collection happens via a pull model over HTTP
- [pushing](https://prometheus.io/docs/instrumenting/pushing/) time series is supported via an intermediary gateway
- targets are discovered via service discovery or static configuration
- multiple modes of graphing and dashboarding support

The package define the following interfaces and types:
- __IPrometheusMetricsRegistry__: Manage the registrations of [Prometheus](https://prometheus.io/) metrics.
- __ICounterCollection__: Manage [Prometheus](https://prometheus.io/)'s [Counter](https://prometheus.io/docs/concepts/metric_types/#counter) metric type.
- __IGaugeCollection__: Manage [Prometheus](https://prometheus.io/)'s [Gauge](https://prometheus.io/docs/concepts/metric_types/#gauge) metric type.
- __IHistogramCollection__: Manage [Prometheus](https://prometheus.io/)'s [Histogram](https://prometheus.io/docs/concepts/metric_types/#histogram) metric type.
- __ISummaryCollection__: Manage [Prometheus](https://prometheus.io/)'s [Summary](https://prometheus.io/docs/concepts/metric_types/#summary) metric type.

### Usage in [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/introduction)
__1. step - Install NuGet:__ [Netension.Monitoring.Prometheus](https://www.nuget.org/packages/Netension.Monitoring.Prometheus/) NuGet package.

__2. step - Register metrics:__ Register neccessary types and metrics with ```AddPrometheusMetrics()``` [```IServiceCollection```](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-3.1) extension method.
```csharp
public void Configure(IServiceCollection services)
{
    services.AddPrometheusMetrics((registry) => 
    {
        registry.RegisterCounter("counter", "Example Counter.");
        registry.RegisterGauge("gauge", "Example Gauge.");
        registry.RegisterHistogram("histogram", "Example Histogram.");
        registry.RegisterSummary("summary", "Example Summary.");
    });
}
```

__3. step - Push metric value:__ Push metric value via metric collections.

```csharp
public class ExampleClass
{
   private readonly ICounterCollection _counterCollection;
   private readonly IGaugeCollection _gaugeCollection ;
   private readonly IHistogramCollection _histogramCollection ;
   private readonly ISummaryCollection _summaryCollection ;

    public ExampleClass(ICounterCollection counterCollection, IGaugeCollection gaugeCollection, IHistogramCollection histogramCollection, ISummaryCollection summaryCollection)
    {
        _counterCollection = counterCollection;
        _gaugeCollection = gaugeCollection;
        _histogramCollection = histogramCollection;
        _summaryCollection = summaryCollection;
    }

    public void Method()
    {
        // Push Counter value
        _counterCollection.Increase("counter");

        // Push Gauge value
        _gaugeCollection.Increase("gauge");

        // Push Histogram value
        _histogramCollection.Increase("histogram", 5.0);

        // Push Summary value
        _summaryCollection.Increase("summary", 5.0);

       // Push duration
       using (_summaryCollection.StartDurationMeasurement("summary"))
       {
           // Do something
       }
    }
}
```
