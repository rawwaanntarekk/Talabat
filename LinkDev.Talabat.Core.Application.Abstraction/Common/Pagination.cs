using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common
{
	public class Pagination<T>
	{
		public Pagination(int pageIndex, int pageSize, IEnumerable<T> data, int _count)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
			Count = _count;
		}

		public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
