using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Core.Aggregates;

/// <summary>
/// Represents a unique install of the software.
/// </summary>
public class InstallLog : Item
{
  private string? _patchVersion;

  /// <summary>
  /// The ID of the user installing the software.
  /// </summary>
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The ID of the machine where the software is being installed.
  /// </summary>
  public string MachineId { get; set; } = null!;

  /// <summary>
  /// The official release number of the software when the record was created. eg. 53000
  /// </summary>
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The patch version of the software when the record was created. eg. A
  /// </summary>
  public string? PatchVersion
  {
    get => _patchVersion;
    set => _patchVersion = value?.ToLower() ?? string.Empty;
  }

  /// <summary>
  /// The name of the software being installed when the record was created.
  /// </summary>
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// The date and time the record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; private set; }

  protected override string GetPartitionKeyValue() => base.GetPartitionKeyValue();

  public InstallLog()
  {
    // The date the log is created.
    DateCreatedUtc = DateTime.UtcNow;
  }
}

