using Autofac;
using FirstRatePlus.LoggingTelemetry.Core.Interfaces;
using FirstRatePlus.LoggingTelemetry.Core.Services;

namespace FirstRatePlus.LoggingTelemetry.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();

    builder.RegisterType<DeleteContributorService>()
        .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
