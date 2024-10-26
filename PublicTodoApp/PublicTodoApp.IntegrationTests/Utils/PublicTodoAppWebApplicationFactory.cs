using Microsoft.AspNetCore.Mvc.Testing;

namespace PublicTodoApp.IntegrationTests.Utils;

public class PublicTodoAppWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DatabaseFixture databaseFixture;
    private IConfiguration configuration;

    public PublicTodoAppWebApplicationFactory(DatabaseFixture databaseFixture)
    {
        this.databaseFixture = databaseFixture;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(delegate (WebHostBuilderContext _, IConfigurationBuilder configBuilder)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.IntegrationTest.json");
            configBuilder.AddJsonFile(path);

            configuration = configBuilder.Build();
        });

        builder.ConfigureServices(services =>
        {
            configuration["ConnectionStrings:DefaultConnection"] = databaseFixture.TestConnectionString;

            //services.AddSingleton<IStartupFilter>(new AutoAuthorizeStartupFilter());
        });
    }
}
