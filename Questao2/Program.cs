using System.Text.Json;
using System;
using System.IO;
using System.Net.Http.Json;

public class Program
{
    static HttpClient client = new HttpClient();

    public static void Main()
    {

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        var totalGoals = getTotalScoredGoalsAsync(teamName, year).Result;

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoalsAsync(teamName, year).Result;

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        var totalGoals = 0;
        var result = await client.GetFromJsonAsync<FootballMatches>($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}");

        if (result == null) throw new Exception("Invalid call requested.");

        foreach (var m in result.data)
        {
            totalGoals += m.Team1Goals;
        }   
        
        return totalGoals;
    }

    public record FootballMatches(int total_pages, List<Competition> data);

    public class Competition {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Team1Goals { get; set; }
        public int Team2Goals { get; set; }
        public int Year { get; set; }

    }
}