using System;
using DakarRally.Api.Adapters;
using DakarRally.Api.DataContracts.DTOs;
using DakarRally.Api.DataContracts.In;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DakarRally.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly Simulator _raceSimulator;
        private readonly StatsGenerator _statsGenerator;

        public RaceController(Simulator raceSimulator, StatsGenerator statsGenerator)
        {
            _raceSimulator = raceSimulator;
            _statsGenerator = statsGenerator;
        }
        
        [HttpPost]
        [Route("create")]
        public ActionResult Create(int year)
        {
            if (year < DateTime.Now.Year)
                return BadRequest("Year cannot be in the past.");
            int id = _raceSimulator.Create(year);
            return Ok(id);
        }

        [HttpPost]
        [Route("addVehicle")]
        public ActionResult AddVehicle([FromBody] VehicleRequest vehicleRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            VehicleAdapter adapter = new VehicleAdapter();
            Vehicle vehicle = adapter.AdaptNew(vehicleRequest);
            int id = _raceSimulator.AddVehicle(vehicleRequest.RaceId, vehicle);
            return Ok(id);
        }

        [HttpDelete]
        [Route("removeVehicle")]
        public ActionResult RemoveVehicle(int vehicleId)
        {
            _raceSimulator.RemoveVehicle(vehicleId);
            return Ok();
        }

        [HttpPost]
        [Route("start")]
        public ActionResult Start(int id)
        {
            _raceSimulator.Start(id);
            return Ok();
        }

        [HttpGet]
        [Route("leaderboard")]
        public ActionResult Leaderboard(int id)
        {
            Leaderboard leaderboard =_statsGenerator.GetRaceLeaderboard(id);
            LeaderboardAdapter adapter = new LeaderboardAdapter();
            LeaderboardDTO leaderboardDTO = adapter.Adapt(leaderboard); 
            return Ok(leaderboardDTO);
        }

        [HttpGet]
        [Route("leaderboard/type")]
        public ActionResult LeaderboardByType(int id, int type)
        {
            Leaderboard leaderboard = _statsGenerator.GetRaceLeaderboard(id, type);
            LeaderboardAdapter adapter = new LeaderboardAdapter();
            LeaderboardDTO leaderboardDTO = adapter.Adapt(leaderboard);
            return Ok(leaderboardDTO);
        }

        [HttpGet]
        [Route("status")]
        public ActionResult Status(int id)
        {
            RaceStatusStat race =_statsGenerator.GetRaceStatus(id);
            RaceStatusAdapter raceStatusAdapter = new RaceStatusAdapter();
            RaceStatusDTO raceStatusDto = raceStatusAdapter.Adapt(race);
            return Ok(raceStatusDto);
        }
    }
}