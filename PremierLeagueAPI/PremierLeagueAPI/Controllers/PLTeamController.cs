using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremierLeagueAPI.Models;

namespace PremierLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLTeamController : ControllerBase
    {
        private static List<PLTeam> plTeams = new List<PLTeam> {
                new PLTeam
                {
                    Id = 441,
                    Name = "Chelsea"

                },
                new PLTeam
                {
                    Id = 442,
                    Name = "Mancheter United"

                },
                new PLTeam
                {
                    Id = 443,
                    Name = "Arsenal"

                }
            };

        [HttpGet]
        public async Task<ActionResult<List<PLTeam>>> GetAllPLTeams()
        {
            return Ok(plTeams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PLTeam>> GetAllPLTeamById(int id)
        {
            var plTeam = plTeams.Find(x => x.Id == id);
            return Ok(plTeam);
        }
    }
}
