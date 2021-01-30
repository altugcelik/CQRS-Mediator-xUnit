using MediatR;

namespace Campaign.Core.Events
{
    public class OrderCreatedEvent : INotification
    {
        public string ProductCode { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public OrderCreatedEvent(string productCode, int quantity, decimal price)
        {
            ProductCode = productCode;
            Quantity = quantity;
            Price = price;
        }
    }
}
 