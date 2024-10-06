namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
