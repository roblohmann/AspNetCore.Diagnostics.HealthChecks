namespace HealthChecks.Gremlin.Tests.DependencyInjection
{
    public class gremlin_registration_should
    {
        [Fact]
        public void add_health_check_when_properly_configured()
        {
            var services = new ServiceCollection();
            services.AddHealthChecks()
                .AddGremlin(_ => new GremlinOptions
                {
                    Hostname = "localhost",
                    Port = 8182,
                    EnableSsl = false
                });

            using var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

            var registration = options.Value.Registrations.First();
            var check = registration.Factory(serviceProvider);

            registration.Name.ShouldBe("gremlin");
            check.ShouldBeOfType<GremlinHealthCheck>();
        }
        [Fact]
        public void add_named_health_check_when_properly_configured()
        {
            var services = new ServiceCollection();
            services.AddHealthChecks()
                .AddGremlin(_ => new GremlinOptions
                {
                    Hostname = "localhost",
                    Port = 8182,
                    EnableSsl = false
                },
                name: "my-gremlin");

            using var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();

            var registration = options.Value.Registrations.First();
            var check = registration.Factory(serviceProvider);

            registration.Name.ShouldBe("my-gremlin");
            check.ShouldBeOfType<GremlinHealthCheck>();
        }
    }
}
