using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FirstRatePlus.LoggingTelemetry.Api.ApiModels;

/// <summary>
/// Represents a paged response.
/// </summary>
/// <typeparam name="T">The type represented in the collection.</typeparam>
public class PagedResponse<T>
{
  /// <summary>
  /// The current page.
  /// </summary>
  [Required]
  [JsonProperty(Order = 0, Required = Required.Always)]
  public int Page { get; private set; }

  /// <summary>
  /// The page size.
  /// </summary>
  [Required]
  [JsonProperty(Order = 1, Required = Required.Always)]
  public int PageSize { get; private set; }

  /// <summary>
  /// The total number of pages.
  /// </summary>
  [Required]
  [JsonProperty(Order = 2, Required = Required.Always)]
  public int TotalPages { get; private set; }

  /// <summary>
  /// The total number of items.
  /// </summary>
  [Required]
  [JsonProperty(Order = 3, Required = Required.Always)]
  public int Total { get; private set; }

  /// <summary>
  /// The collection of items.
  /// </summary>
  [Required]
  [JsonProperty(Order = 4, Required = Required.Always)]
  public IEnumerable<T> Data { get; set; }

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="items">The collection of items.</param>
  /// <param name="count">The total number of items matching the request.</param>
  /// <param name="pageNumber">The current page number.</param>
  /// <param name="pageSize">The page size.</param>
  public PagedResponse(List<T> items, int count, int pageNumber, int pageSize)
  {
    Total = count;
    PageSize = pageSize;
    Page = pageNumber;
    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    Data = items;
  }

  /// <summary>
  /// Converts a collection of items to a paged response.
  /// </summary>
  /// <param name="source">The source query.</param>
  /// <param name="pageNumber">The current page number.</param>
  /// <param name="pageSize">The page size.</param>
  /// <returns></returns>
  public static PagedResponse<T> ToPagedResponse(IQueryable<T> source, int pageNumber, int pageSize)
  {
    var count = source.Count();
    var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    return new PagedResponse<T>(items, count, pageNumber, pageSize);
  }
}

