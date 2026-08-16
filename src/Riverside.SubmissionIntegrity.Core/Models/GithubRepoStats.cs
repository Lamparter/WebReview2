namespace Riverside.SubmissionIntegrity.Core.Models;

public class GitCommit
{
	public string Message { get; set; } = string.Empty;
	public int Additions { get; set; }
	public int Deletions { get; set; }
	public DateTime Date { get; set; }
}

public class GithubRepoStats
{
	public bool IsPublic { get; set; }
	public bool HasOssLicense { get; set; }
	public string ReadmeUrl { get; set; } = string.Empty;
	public List<GitCommit> RecentCommits { get; set; } = new();
}
