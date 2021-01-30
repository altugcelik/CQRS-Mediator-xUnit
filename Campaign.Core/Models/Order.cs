namespace Campaign.Core.Models
{
    public class Order : EntityBase
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
 