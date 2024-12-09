namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    // This will be owned entity by order entity
    public class Address : BaseEntity<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
