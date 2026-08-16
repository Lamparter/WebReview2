using Riverside.SubmissionIntegrity.Core.Models;

namespace Riverside.SubmissionIntegrity.Core.Services;

public class MockHackatimeService : IHackatimeService
{
	public Task<HackatimeStats> GetStatsAsync(string projectNames)
	{
		return Task.FromResult(new HackatimeStats
		{
			EstimatedAiUsagePercentage = 15.5,
			EditorsUsed = new List<string> { "VS Code", "Visual Studio" },
			OsPlatformsUsed = new List<string> { "Windows", "Mac" },
			TotalHoursLogged = 25.5
		});
	}
}
