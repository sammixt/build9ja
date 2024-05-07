using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Specifications
{
    public class DataTableRequestSpecification
    {
        public string Draw {get; set;}
        public string Start {get; set;}
        public string Length {get; set;}
        public string  SortColumn {get; set;}
        public string  SortColumnDirection {get; set;}
        public string  SearchValue {get; set;}
        public int PageSize { get; set;} 
         public int  Skip {get; set;}
    }
}