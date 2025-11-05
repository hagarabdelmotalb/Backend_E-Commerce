namespace Domain.Exepctions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string Id)
            : base($"Basket with id: {Id} was not found.")
        {
        }
    }
}
