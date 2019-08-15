using DakarRally.Api.Adapters;
using DakarRally.Api.DataContracts.DTOs;
using DakarRally.Api.DataContracts.In;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DakarRally.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly Simulator _raceSimulator;
        private readonly StatsGenerator _statsGenerator;

        public VehicleController(Simulator raceService, StatsGenerator statsGenerator)
        {
            _raceSimulator = raceService;
            _statsGenerator = statsGenerator;
        }
        
        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] UpdateVehicleRequest updateVehicleRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            VehicleAdapter adapter = new VehicleAdapter();
            Vehicle newVehicle = adapter.AdaptUpdate(updateVehicleRequest);
            _raceSimulator.UpdateVehicle(newVehicle);
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public ActionResult Get(string teamName, string model, string manufactoringDate, string status, string distance, string orderBy)
        {
            List<Vehicle> vehicles = _statsGenerator.GetVehicles(teamName, model, manufactoringDate, status, distance, orderBy);
            VehicleAdapter adapter = new VehicleAdapter();
            List<VehicleDTO> vehcleDTOs = adapter.ToVehicleDTO(vehicles);
            return Ok(vehcleDTOs);
        }

        [HttpGet]
        [Route("stats")]
        public ActionResult GetStats(int vehicleId)
        {
            Vehicle vehicle = _statsGenerator.GetVehicle(vehicleId);
            VehicleAdapter adapter = new VehicleAdapter();
            VehicleStatDTO vehicleStatDTO = adapter.ToVehicleStatDTO(vehicle);
            return Ok(vehicleStatDTO);
        }
    }
}