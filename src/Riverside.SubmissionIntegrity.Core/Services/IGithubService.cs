using Riverside.SubmissionIntegrity.Core.Models;

namespace Riverside.SubmissionIntegrity.Core.Services;

public interface IGithubService
{
	Task<GithubRepoStats> GetRepoStatsAsync(string repositoryUrl);
}
