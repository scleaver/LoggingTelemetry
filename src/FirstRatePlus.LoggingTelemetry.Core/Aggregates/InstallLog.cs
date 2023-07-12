using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Core.Entities;

/// <summary>
/// Represents a unique install of the software.
/// </summary>
public class InstallLog : Item //EntityBase<Guid>, IAggregateRoot
{
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
  /// The name of the software being installed when the record was created.
  /// </summary>
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// The date and time the record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; private set; }

  protected override string GetPartitionKeyValue() => base.GetPartitionKeyValue();

  ///// <summary>
  ///// Constructor.
  ///// </summary>
  ///// <param name="userId">The user Id.</param>
  ///// <param name="machineId">The machine ID.</param>
  ///// <param name="releaseNumber">The sofwtare release number.</param>
  ///// <param name="sofwareName">The sofwtare name.</param>
  //public InstallLog(string userId, string machineId, int releaseNumber, string softwareName)
  //{
  //  UserId = userId;
  //  MachineId = machineId;
  //  ReleaseNumber = releaseNumber;
  //  SoftwareName = softwareName;

  //  // The date the log is created.
  //  DateCreatedUtc = DateTime.UtcNow;
  //}

  public InstallLog()
  {


    // The date the log is created.
    DateCreatedUtc = DateTime.UtcNow;
  }
}

