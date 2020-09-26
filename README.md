![nuget-template](https://github.com/Netension/monitoring/blob/develop/banner.png)
__Netenson.Monitoring__ packages provide types that make easier the metrics integration.

A software metric is a measure of software characteristics which are quantifiable or countable. Software metrics are important for measuring software performance.

![Publish](https://github.com/Netension/monitoring/workflows/Publish/badge.svg)<br/>
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=alert_status)](https://sonarcloud.io/dashboard?id=Netension_monitoring)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=coverage)](https://sonarcloud.io/dashboard?id=Netension_monitoring)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Netension_monitoring&metric=bugs)](https://sonarcloud.io/dashboard?id=Netension_monitoring)

# Packages
## [Netension.Monitoring.Core](https://www.nuget.org/packages/Netension.Monitoring.Core/)![Nuget](https://img.shields.io/nuget/v/Netension.Monitoring.Core?label=Netension.Monitoring.Core&style=plastic)
Provides generally used types for metrics produce.

### StopwatchCollection
Supports to manage multiple stopwatches in an application.

## [Netension.Monitoring.Prometheus](https://www.nuget.org/packages/Netension.Monitoring.Prometheus/)![Nuget](https://img.shields.io/nuget/v/Netension.Monitoring.Prometheus?label=Netension.Monitoring.Prometheus&style=plastic)
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
