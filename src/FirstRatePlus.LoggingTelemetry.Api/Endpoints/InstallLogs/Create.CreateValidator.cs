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
      .MaximumLength(68)
      .WithMessage("The user ID has a maximum length of 68 characters.")
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

    RuleFor(x => x.PatchVersion)
      .MaximumLength(2)
      .WithMessage("The patch version has a maximum length of 2 characters.");

    RuleFor(x => x.SoftwareName)
      .NotEmpty()
      .WithMessage("A software name is required.")
      .MinimumLength(1)
      .WithMessage("The software name must have a minimum length of 1 character.");
  }
}