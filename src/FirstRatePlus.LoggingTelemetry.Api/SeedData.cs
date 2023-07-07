using Bogus;
using FirstRatePlus.LoggingTelemetry.Core.Entities;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api;

/// <summary>
/// Seeds the DB with test data for development.
/// </summary>
public static class SeedData
{

  public static async Task InitializeAsync(IRepositoryFactory factory)
  {
    await PopulateTestInstallLogsAsync(factory);
  }

  private static async Task PopulateTestInstallLogsAsync(IRepositoryFactory factory)
  {
    IRepository<InstallLog> repository = factory.RepositoryOf<InstallLog>();

    await SeedAsync();

    async Task SeedAsync()
    {
      IEnumerable<InstallLog> current = await repository.GetAsync(x => x.Type == nameof(InstallLog));

      if (current.Any())
      {
        return;
      }

      Faker<InstallLog> installLogFaker = new();
      installLogFaker
          .StrictMode(true)
          .RuleFor(i => i.UserId, f => Guid.NewGuid().ToString())
          .RuleFor(i => i.MachineId, f => Guid.NewGuid().ToString())
          .RuleFor(i => i.SoftwareName, f => "FirstRate5")
          .RuleFor(p => p.ReleaseNumber, f => f.Random.Number(53000, 55000));

      List<InstallLog> logs = installLogFaker.Generate(100);
      await repository.CreateAsync(logs);
    }
  }
}
