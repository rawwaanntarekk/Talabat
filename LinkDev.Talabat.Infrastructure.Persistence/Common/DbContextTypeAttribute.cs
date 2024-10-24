namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    public class DbContextTypeAttribute : Attribute
    {
        public Type DbContextType { get; set; }

        public DbContextTypeAttribute(Type dbContextType)
        {
            DbContextType = dbContextType;
        }
    }
}
