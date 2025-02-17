using Microsoft.Azure.Devices;

namespace HealthChecks.Azure.IoTHub
{
    public class IoTHubOptions
    {
        internal string ConnectionString { get; private set; } = null!;
        internal bool RegistryReadCheck { get; private set; }
        internal bool RegistryWriteCheck { get; private set; }
        internal bool ServiceConnectionCheck { get; private set; }
        internal string RegistryReadQuery { get; private set; } = null!;
        internal Func<string> RegistryWriteDeviceIdFactory { get; private set; } = null!;
        internal TransportType ServiceConnectionTransport { get; private set; }

        public IoTHubOptions AddConnectionString(string connectionString)
        {
            ConnectionString = Guard.ThrowIfNull(connectionString);
            return this;
        }

        public IoTHubOptions AddRegistryReadCheck(string query = "SELECT deviceId FROM devices")
        {
            RegistryReadCheck = true;
            RegistryReadQuery = query;
            return this;
        }

        public IoTHubOptions AddRegistryWriteCheck(Func<string>? deviceIdFactory = null)
        {
            RegistryWriteCheck = true;
            RegistryWriteDeviceIdFactory = deviceIdFactory ?? (() => "health-check-registry-write-device-id");
            return this;
        }

        public IoTHubOptions AddServiceConnectionCheck(TransportType transport = TransportType.Amqp)
        {
            ServiceConnectionCheck = true;
            ServiceConnectionTransport = transport;
            return this;
        }
    }
}
