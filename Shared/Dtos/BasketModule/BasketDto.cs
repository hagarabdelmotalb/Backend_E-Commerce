namespace Shared.Dtos.BasketModule
{
    public record BasketDto
    {
        public string Id { get; set; } 
        public ICollection<BasketItemDto> basketItems { get; set; } = [];
    }
}
