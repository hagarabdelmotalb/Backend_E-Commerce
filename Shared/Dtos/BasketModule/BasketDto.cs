namespace Shared.Dtos.BasketModule
{
    public record BasketDto
    {
        public int Id { get; set; }
        public ICollection<BasketItemDto> basketItems { get; set; } = [];
    }
}
