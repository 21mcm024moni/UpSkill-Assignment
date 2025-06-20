namespace OrderProcessingSystem.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public ProductType ProductType { get; set; }
        public string? ProductName { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal Amount { get; set; }
        public string? Agent { get; set; } = "Lisa";
    }

    public enum ProductType
    {
        PhysicalProduct,
        Book,
        Membership,
        MembershipUpgrade,
        Video
    }
}
