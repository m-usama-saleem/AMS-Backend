using Appointment_Management_System.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Appointment_Management_System.IntegrationTests;

public class IntegrationTestFixture : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    protected readonly HttpClient Client;
    private readonly MsSqlContainer _sqlContainer;

    public IntegrationTestFixture(WebApplicationFactory<Program> factory)
    {
        _sqlContainer = new MsSqlBuilder()
            .WithPassword("Your_password123")
            .Build();
        _sqlContainer.StartAsync().GetAwaiter().GetResult();

        var clientFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace DB Context
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                if (descriptor != null) services.Remove(descriptor);

                var connectionString = BuildConnectionString(_sqlContainer.GetConnectionString(), "AMS");
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(connectionString));
            });
        });

        Client = clientFactory.CreateClient();
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _sqlContainer.DisposeAsync().GetAwaiter().GetResult();
    }
    
    private static string BuildConnectionString(string originalConnStr, string targetDbName)
    {
        var builder = new SqlConnectionStringBuilder(originalConnStr)
        {
            InitialCatalog = targetDbName,
            DataSource = "127.0.0.1,1433"
        };

        return builder.ToString();
    }
}