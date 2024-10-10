namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
