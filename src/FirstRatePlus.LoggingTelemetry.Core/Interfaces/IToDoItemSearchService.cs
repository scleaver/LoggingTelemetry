using Ardalis.Result;
using FirstRatePlus.LoggingTelemetry.Core.ProjectAggregate;

namespace FirstRatePlus.LoggingTelemetry.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
