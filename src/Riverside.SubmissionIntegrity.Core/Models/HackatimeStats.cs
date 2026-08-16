namespace Riverside.SubmissionIntegrity.Core.Models;

public class HackatimeStats
{
	public double EstimatedAiUsagePercentage { get; set; }
	public List<string> EditorsUsed { get; set; } = new();
	public List<string> OsPlatformsUsed { get; set; } = new();
	public double TotalHoursLogged { get; set; }
}
