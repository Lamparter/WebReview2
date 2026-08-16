using Riverside.SubmissionIntegrity.Core.Models;

namespace Riverside.SubmissionIntegrity.Core.Services;

public class MockGithubService : IGithubService
{
	public Task<GithubRepoStats> GetRepoStatsAsync(string repositoryUrl)
	{
		return Task.FromResult(new GithubRepoStats
		{
			IsPublic = true,
			HasOssLicense = true,
			ReadmeUrl = repositoryUrl.TrimEnd('/') + "/blob/main/README.md",
			RecentCommits = new List<GitCommit>
			{
				new GitCommit { Message = "Initial commit", Additions = 500, Deletions = 0, Date = DateTime.Now.AddDays(-2) },
				new GitCommit { Message = "Add core features", Additions = 1200, Deletions = 300, Date = DateTime.Now.AddDays(-1) },
				new GitCommit { Message = "Fix bugs and UI", Additions = 250, Deletions = 150, Date = DateTime.Now }
			}
		});
	}
}
