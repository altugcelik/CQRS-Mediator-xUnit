using MediatR;

namespace Campaign.Core.Events
{
    public class ProductCreatedEvent: INotification
    {
        private readonly string _productName;

        public ProductCreatedEvent(string productName)
        {
            _productName = productName;
        }
    }
}
 