namespace LinkDev.Talabat.Core.Application.Abstraction.Models._Common
{
    public class AddressDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
