using System.Reflection;

namespace PremierLeagueAPI.Services.PremierLeagueService
{
    public class PremierLeagueService : IPremierLeagueSevice
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
                    Name = "Manchester United"

                },
                new PLTeam
                {
                    Id = 443,
                    Name = "Arsenal"

                }
            };

        public List<PLTeam> AddNewPLTeam(PLTeam team)
        {
            plTeams.Add(team);
            return plTeams;
        }

        public List<PLTeam> DeletePLTeam(string name)
        {
            // convert first letter of input name to uppercase
            char[] nameArray = name.ToCharArray();
            nameArray[0] = char.ToUpper(nameArray[0]);
            name = new string(nameArray);

            var plTeam = plTeams.Find(x => x.Name == name);
            if (plTeam is null)
                return null;

            plTeams.Remove(plTeam);
            return plTeams;
        }

        public PLTeam GetAllPLTeamById(int id)
        {
            var plTeam = plTeams.Find(x => x.Id == id);
            if (plTeam is null)
                return null;
            return plTeam;
        }

        public PLTeam GetAllPLTeamByName(string name)
        {
            var plTeam = plTeams.Find(x => x.Name == name);
            if (plTeam is null)
                return null;
            return plTeam;
        }

        public List<PLTeam> GetAllPLTeams()
        {
            return plTeams;
        }

        public PLTeam UpdateField(int id, string field, string value)
        {
            var fields = new Dictionary<string, string>()
            {
                {"matchesplayed", "MatchesPlayed"},
                {"goalsscored", "GoalsScored"},
                {"goalsconceded", "GoalsConceded"},
                {"points", "Points"}
            };

            var plTeam = plTeams.Find(x => x.Id == id);
            if (plTeam is null)
                return null;
            // check if field is id {type int}
            field = field.ToLower();

            foreach (var item in fields)
                if (item.Key == field)
                    field = item.Value;

            PropertyInfo attr = plTeam.GetType().GetProperty(field);
            // update field
            int intValue = Int32.Parse(value);
            if (attr != null && attr.CanWrite)
                attr.SetValue(plTeam, intValue);

            return plTeam;
        }
    }
}
