namespace BackendApplication.Schema.Types
{
    public class ContactLensType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Degree { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
