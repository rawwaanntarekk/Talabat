namespace LinkDev.Talabat.Core.Domain.Common
{
	public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public required TKey Id { get; set; }

	}
}
