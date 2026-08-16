using Riverside.SubmissionIntegrity.Core.Models;

namespace Riverside.SubmissionIntegrity.Core.Services;

public interface IHackatimeService
{
	Task<HackatimeStats> GetStatsAsync(string projectNames);
}
