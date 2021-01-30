namespace Campaign.Core.Models
{
    public class Product : EntityBase
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
 