using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Controllers.Resources
{
    public class MakesResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakesResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}
