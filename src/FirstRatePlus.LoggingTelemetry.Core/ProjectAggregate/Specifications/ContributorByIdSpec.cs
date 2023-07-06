using Ardalis.Specification;
using FirstRatePlus.LoggingTelemetry.Core.ContributorAggregate;

namespace FirstRatePlus.LoggingTelemetry.Core.ProjectAggregate.Specifications;

public class ContributorByIdSpec : Specification<Contributor>, ISingleResultSpecification
{
  public ContributorByIdSpec(int contributorId)
  {
    Query
        .Where(contributor => contributor.Id == contributorId);
  }
}
