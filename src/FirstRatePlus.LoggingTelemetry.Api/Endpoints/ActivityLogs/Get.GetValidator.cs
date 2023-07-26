using FastEndpoints;
using FluentValidation;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class GetValidator : Validator<GetActivityLogRequest>
{
  public GetValidator()
  {
    RuleFor(x => x.Id)
    .NotEmpty()
    .WithMessage("An ID is required.")
    .MinimumLength(1)
    .WithMessage("The ID must have a minimum length of 1 character.");
  }
}
