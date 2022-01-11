﻿using System;
namespace build9ja.core.Specifications
{
	public class PaginationSpecification
	{
		public PaginationSpecification()
		{
		}

        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string sort { get; set; }

        private string _search;

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}

