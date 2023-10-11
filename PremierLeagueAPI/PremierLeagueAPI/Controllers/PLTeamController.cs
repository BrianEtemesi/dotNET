using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PremierLeagueAPI.Services.PremierLeagueService;
using System.Reflection;
using System.Xml.Linq;

namespace PremierLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLTeamController : ControllerBase
    {
        private readonly IPremierLeagueSevice _premierLeagueSevice;

        public PLTeamController(IPremierLeagueSevice premierLeagueSevice)
        {
            _premierLeagueSevice = premierLeagueSevice;
        }

        [HttpGet("PLTeams")]
        public async Task<ActionResult<List<PLTeam>>> GetAllPLTeams()
        {
            var result = _premierLeagueSevice.GetAllPLTeams();
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<PLTeam>> GetAllPLTeamById(int id)
        {
            var result = _premierLeagueSevice.GetAllPLTeamById(id);
            if (result is null)
                return NotFound($"Sorry, id {id} doesn't exist");
            return Ok(result);
        }

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<PLTeam>> GetAllPLTeamByName(string name)
        {
            var result = _premierLeagueSevice.GetAllPLTeamByName(name);
            if (result is null)
                return NotFound($"Sorry, {name} is not in the current Premier League");
            return Ok(result);
        }

        [HttpPost("AddPLTeam")]
        public async Task<ActionResult<List<PLTeam>>> AddNewPLTeam(PLTeam team)
        { 
            var result = _premierLeagueSevice.AddNewPLTeam(team); 
            return Ok(result);
        }

        [HttpPut("UpdateField/{id}")]

        public async Task<ActionResult<PLTeam>> UpdateField(int id, string field, string value)
        {
            var result = _premierLeagueSevice.UpdateField(id, field, value);
            if (result is null)
                return NotFound($"sorry, {id} not a valid id of a current premier league team");
            return Ok(result);
        }

        [HttpDelete("DeletePLTeam/{name}")]
        public async Task<ActionResult<List<PLTeam>>> DeletePLTeam(string name)
        {
            var result = _premierLeagueSevice.DeletePLTeam(name);
            if (result is null)
                return NotFound($"Sorry, {name} is not in the current Premier League");
            return Ok(result);
        }
    }
}
