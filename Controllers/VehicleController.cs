using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Models;
using vega.Repositories;

namespace vega.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVehicleRepository repository;

        public VehicleController(IMapper mapper,IUnitOfWork unitOfWork , IVehicleRepository repository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult CreatVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            unitOfWork.Complete();

            vehicle = repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = repository.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            mapper.Map(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            unitOfWork.Complete();

            vehicle = repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = repository.GetVehicle(id, false);
            if(vehicle == null)
            {
                return NotFound();
            }

            repository.Remove(vehicle);
            unitOfWork.Complete();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            var vehicle = repository.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpGet]
        public IEnumerable<VehicleResource> GetVehicles([FromHeader]VehicleQuery filter)
        {
            var vehicles = repository.GetVehicles(filter);

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }
    }
}
