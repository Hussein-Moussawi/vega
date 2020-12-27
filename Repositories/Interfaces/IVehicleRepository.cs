using System.Collections.Generic;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Repositories
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        Vehicle GetVehicle(int id, bool includeRelations = true);
        void Remove(Vehicle vehicle);
        IEnumerable<Vehicle> GetVehicles(VehicleQuery filter);
    }
}