namespace Shared.Dtos.ProductModule
{
    public record BrandResultDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
