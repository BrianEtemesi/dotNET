namespace PremierLeagueAPI.Services.PremierLeagueService
{
    public interface IPremierLeagueSevice
    {
        List<PLTeam> GetAllPLTeams();
        PLTeam GetAllPLTeamById(int id);
        PLTeam GetAllPLTeamByName(string name);
        PLTeam AddNewPLTeam(PLTeam team);
        PLTeam UpdateField(int id, string field, string value);
        PLTeam DeletePLTeam(string name);

    }
}
