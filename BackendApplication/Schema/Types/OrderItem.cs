namespace BackendApplication.Schema.Types
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ContactLensTypeId { get; set; }
        public ContactLensType ContactLensType { get; set; }
        public int OrderQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
