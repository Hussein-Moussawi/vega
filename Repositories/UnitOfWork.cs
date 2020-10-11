using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Context;

namespace vega.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext vegaDbContext;

        public UnitOfWork(VegaDbContext vegaDbContext)
        {
            this.vegaDbContext = vegaDbContext;
        }

        public void Complete()
        {
            vegaDbContext.SaveChanges();
        }
    }
}
