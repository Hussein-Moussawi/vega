using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class SortQuery
    {
        public string SortBy { get; set; }

        public bool IsSortByAsc { get; set; }
    }
}
