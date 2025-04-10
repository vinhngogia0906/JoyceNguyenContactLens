namespace BackendApplication.Schema.Types
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
