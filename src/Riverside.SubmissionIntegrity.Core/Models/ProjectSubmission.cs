namespace Riverside.SubmissionIntegrity.Core.Models;

public class ProjectSubmission
{
	public string ProjectName { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string DemoUrl { get; set; } = string.Empty;
	public string RepositoryUrl { get; set; } = string.Empty;
	public double HoursSpent { get; set; }
	public string Platform { get; set; } = string.Empty;
	public string ScreenshotUrl { get; set; } = string.Empty;
	public string HackatimeProjectNames { get; set; } = string.Empty;
	public string SlackId { get; set; } = string.Empty;
	public string ReadmeUrl { get; set; } = string.Empty;
}
