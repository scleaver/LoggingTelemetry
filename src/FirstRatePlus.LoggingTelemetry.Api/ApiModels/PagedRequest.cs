using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FirstRatePlus.LoggingTelemetry.Api.ApiModels;

/// <summary>
/// Represents a paged request.
/// </summary>
public abstract class PagedRequest
{
  private int _page;
  private int _pageSize;

  /// <summary>
  /// The page to retrieve. Default is 1.
  /// </summary>
  [JsonProperty(Order = 0)]
  public int Page
  {
    get { return _page; }
    set { _page = value < 1 ? 1 : value; }
  }

  /// <summary>
  /// The number of items to retrieve per page. Default is 100, minimum is 1 and max is 1000.
  /// </summary>
  [JsonProperty(Order = 0)]
  public int PageSize
  {
    get { return _pageSize; }
    set { _pageSize = value < 1 || value > 1000 ? 1000 : value; }
  }

  /// <summary>
  /// The constructor.
  /// </summary>
  /// <param name="page">The page to return.</param>
  /// <param name="pageSize">The page size.</param>
  public PagedRequest(int? page = null, int? pageSize = null)
  {
    Page = page ?? 1;
    PageSize = pageSize ?? 100;
  }
}

