using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class VehicleQuery : SortQuery

    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

    }
}
