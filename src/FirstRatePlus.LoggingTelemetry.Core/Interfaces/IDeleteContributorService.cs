using Ardalis.Result;

namespace FirstRatePlus.LoggingTelemetry.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
