using Bogus;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;
using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api;

/// <summary>
/// Seeds the DB with test data for development.
/// </summary>
public static class SeedData
{

  public static async Task InitializeAsync(IRepositoryFactory factory)
  {
    await PopulateTestInstallLogsAsync(factory);
    await PopulateTestActivityLogsAsync(factory);
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

      Faker<InstallLog> logFaker = new();
      logFaker
          .RuleFor(i => i.UserId, f => Guid.NewGuid().ToString())
          .RuleFor(i => i.MachineId, f => Guid.NewGuid().ToString())
          .RuleFor(i => i.SoftwareName, f => "FirstRate5")
          .RuleFor(p => p.ReleaseNumber, f => f.Random.Number(53000, 55000));

      List<InstallLog> logs = logFaker.Generate(100);
      await repository.CreateAsync(logs);
    }
  }

  private static async Task PopulateTestActivityLogsAsync(IRepositoryFactory factory)
  {
    IRepository<ActivityLog> repository = factory.RepositoryOf<ActivityLog>();

    await SeedAsync();

    async Task SeedAsync()
    {
      IEnumerable<ActivityLog> current = await repository.GetAsync(x => x.Type == nameof(ActivityLog));

      if (current.Any())
      {
        return;
      }

      var activities = new[] { "Calculation", "ChangeAddress", "ChangeAccreditation" };

      // Create a dynamic JSON object
      dynamic jsonData = new JObject();
      jsonData.Property1 = "Value 1";
      jsonData.Property2 = 123;
      jsonData.Property3 = new JArray("Item 1", "Item 2", "Item 3");

      Faker<ActivityLog> logFaker = new();
      logFaker
          .RuleFor(i => i.UserId, f => Guid.NewGuid().ToString())
          .RuleFor(i => i.ActivityType, f => f.PickRandom(activities))
          .RuleFor(i => i.ActivityDateUtc, f => f.Date.Past(1))
          .RuleFor(i => i.Data, f => JObject.FromObject(jsonData))
          .RuleFor(i => i.SoftwareName, f => "FirstRate5")
          .RuleFor(p => p.ReleaseNumber, f => f.Random.Number(53000, 55000));

      List<ActivityLog> logs = logFaker.Generate(100);
      await repository.CreateAsync(logs);
    }
  }
}
