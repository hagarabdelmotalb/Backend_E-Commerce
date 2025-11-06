namespace Shared
{
    public record PaginatedResult<TData>(int PageIndex,int Pagesize ,int TotalCount,IEnumerable<TData> Data)
    {
    }
} 
