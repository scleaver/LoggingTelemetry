using System;
using FastEndpoints;
using FluentValidation;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class CreateValidator : Validator<CreateInstallLogRequest>
{
  public CreateValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty()
      .WithMessage("A user id is required.")
      .MinimumLength(1)
      .WithMessage("The user ID must have a minimum length of 1 character.");

    RuleFor(x => x.MachineId)
      .NotEmpty()
      .WithMessage("A machine ID is required.")
      .MinimumLength(1)
      .WithMessage("The machine ID must have a minimum length of 1 character.");

    RuleFor(x => x.ReleaseNumber)
      .GreaterThan(0)
      .WithMessage("The release number must be greater than 0.")
      .LessThan(1000000)
      .WithMessage("The release number must be greater than 1M.");

    RuleFor(x => x.SoftwareName)
      .NotEmpty()
      .WithMessage("A software name is required.")
      .MinimumLength(1)
      .WithMessage("The software name must have a minimum length of 1 character.");
  }
}