using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Riverside.SubmissionIntegrity.Core.Models;
using Riverside.SubmissionIntegrity.Core.Services;

namespace Riverside.SubmissionIntegrity.Core.ViewModels;

public partial class ReviewViewModel : ObservableObject
{
	private readonly IGithubService _githubService;
	private readonly IHackatimeService _hackatimeService;

	[ObservableProperty]
	private ProjectSubmission submission = new();

	[ObservableProperty]
	private GithubRepoStats? githubStats;

	[ObservableProperty]
	private HackatimeStats? hackatimeStats;

	[ObservableProperty]
	private string testingExtractionSummary = string.Empty;

	[ObservableProperty]
	private bool hasExtractionError;

	[ObservableProperty]
	private bool flaggedForAiUsage;

	[ObservableProperty]
	private bool flaggedForNoLicense;

	[ObservableProperty]
	private string shipJustificationDraft = string.Empty;

	[ObservableProperty]
	private string finalRecommendation = string.Empty;

	public ReviewViewModel(IGithubService githubService, IHackatimeService hackatimeService)
	{
		_githubService = githubService;
		_hackatimeService = hackatimeService;
	}

	[RelayCommand]
	public async Task AnalyzeSubmissionAsync()
	{
		if (string.IsNullOrWhiteSpace(Submission.RepositoryUrl))
			return;

		GithubStats = await _githubService.GetRepoStatsAsync(Submission.RepositoryUrl);

		if (string.IsNullOrWhiteSpace(Submission.ReadmeUrl) && GithubStats != null)
		{
			Submission.ReadmeUrl = GithubStats.ReadmeUrl;
		}

		HackatimeStats = await _hackatimeService.GetStatsAsync(Submission.HackatimeProjectNames);

		ExtractTestingSummary();
		PerformValidation();
	}

	private void ExtractTestingSummary()
	{
		HasExtractionError = false;
		if (string.IsNullOrWhiteSpace(Submission.DemoUrl) && string.IsNullOrWhiteSpace(Submission.RepositoryUrl))
		{
			HasExtractionError = true;
			TestingExtractionSummary = "Could not extract testing info: No Demo or Repo provided.";
			return;
		}

		TestingExtractionSummary = $"Test via Demo URL: {Submission.DemoUrl}\n" +
								   $"Review Code at: {Submission.RepositoryUrl}\n" +
								   $"Platform Info: {Submission.Platform}\n" +
								   $"Readme: {Submission.ReadmeUrl}";
	}

	private void PerformValidation()
	{
		FlaggedForAiUsage = HackatimeStats?.EstimatedAiUsagePercentage > 40;
		FlaggedForNoLicense = GithubStats?.HasOssLicense == false;
	}

	[RelayCommand]
	public void GenerateShipJustification(bool commitsAndHoursFeelAppropriate)
	{
		if (FlaggedForAiUsage || FlaggedForNoLicense || !commitsAndHoursFeelAppropriate || (HackatimeStats?.TotalHoursLogged < Submission.HoursSpent * 0.5))
		{
			FinalRecommendation = "Reject";
			ShipJustificationDraft = $"The project does not meet the specified criteria. " +
									 (FlaggedForAiUsage ? "High AI usage detected. " : "") +
									 (FlaggedForNoLicense ? "No OSS license found. " : "") +
									 (!commitsAndHoursFeelAppropriate ? "Commits and logged hours do not align with the claimed scope. " : "");
		}
		else
		{
			FinalRecommendation = "Approve";
			ShipJustificationDraft = $"The project appears valid. Hours match commits, it has an OSS license, and AI usage is within acceptable limits. Demo and Repo check out.";
		}
	}
}
