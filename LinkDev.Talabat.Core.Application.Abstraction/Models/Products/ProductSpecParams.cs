namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Products
{
	public class ProductSpecParams
	{
        public string? Sort { get; set; }

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;

        private const int MaxPagesSize = 10;

        private int _pageSize = 5;

        private string? search;

        public string? Search
        {
            get { return search; }

            // To match the Normalized column in the database
            set { search = value?.ToUpper(); }
        }

        public int PageSize {

            get => _pageSize; 
            
            set { _pageSize = value > MaxPagesSize ? MaxPagesSize : value; }
        }


    }
}
