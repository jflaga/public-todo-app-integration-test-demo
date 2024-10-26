using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;

namespace PublicTodoApp.IntegrationTests.Utils;

public class PublicTodoAppWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly DatabaseFixture databaseFixture;
    private Respawner respawner = default!;
    private IConfiguration configuration;

    public PublicTodoAppWebApplicationFactory(DatabaseFixture databaseFixture)
    {
        this.databaseFixture = databaseFixture;
    }

    public async Task InitializeAsync()
    {
        respawner = await Respawner.CreateAsync(databaseFixture.TestConnectionString);
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

    async Task IAsyncLifetime.DisposeAsync()
    {
        await ResetDatabase();
    }

    private async Task ResetDatabase()
    {
        await respawner.ResetAsync(databaseFixture.TestConnectionString);
    }
}
