using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vega.Context;
using vega.Extensions;
using vega.Models;

namespace vega.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public Vehicle GetVehicle(int id, bool includeRelations = true)
        {
            if (!includeRelations) return context.Vehicles.Find(id);

            return context.Vehicles
                .Include(v => v.VehicleFeatures)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefault(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            context.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }

        public IEnumerable<Vehicle> GetVehicles(VehicleQuery filter)
        {
            var vehicles = context.Vehicles
                .Include(v => v.VehicleFeatures)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make).AsQueryable();

            if (filter.MakeId.HasValue)
                vehicles = vehicles.Where(v => v.Model.MakeId == filter.MakeId.Value);
            if (filter.ModelId.HasValue)
                vehicles = vehicles.Where(v => v.ModelId == filter.ModelId);

            #region Sorting
            var sortLogic = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v => v.Model.MakeId,
                ["model"] = v => v.ModelId,
                ["contactName"] = v => v.ContactName,
            };

            vehicles = vehicles.ApplyOrdering(filter, sortLogic);
            #endregion

            return vehicles;
        }
    }
}
