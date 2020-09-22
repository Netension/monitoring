namespace Netension.Monitoring.Prometheus
{
    public interface ICounter
    {
        void Increase(string key);
        void Increase(string key, double value);
    }
}
