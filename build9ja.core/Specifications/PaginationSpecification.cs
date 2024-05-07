using System;
namespace build9ja.core.Specifications
{
	public class PaginationSpecification
	{
		public PaginationSpecification()
		{
		}

        private const int MaxPageSize = 40;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string sort { get; set; }
        
        public string Brand { get; set; }
        public decimal? Amount { get; set; }
        private string _search;
        private string _searchBy;

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public string SearchBy
        {
            get => _searchBy;
            set => _searchBy = value.ToLower();
        }
    }
}

