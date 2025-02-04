using System;

namespace Core.Specifications;

public class BaseParams
{
    public int Sort { get; set; }
    public int PageIndex { get; set; } = 1;

    private const int MaxPageSize = 1000;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize; set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? Search { get; set; }

}
