namespace PremierLeagueAPI.Models
{
    public class PLTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchesPlayed { get; set; } = 0;
        public int GoalsScored { get; set; } = 0;
        public int GoalsConceded { get; set; } = 0;
        public int Points { get; set; } = 0;

    }
}
