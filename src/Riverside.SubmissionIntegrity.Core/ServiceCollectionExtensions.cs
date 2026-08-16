using Microsoft.Extensions.DependencyInjection;
using Riverside.SubmissionIntegrity.Core.Services;
using Riverside.SubmissionIntegrity.Core.ViewModels;

namespace Riverside.SubmissionIntegrity.Core;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSubmissionIntegrityCore(this IServiceCollection services)
	{
		services.AddTransient<IGithubService, MockGithubService>();
		services.AddTransient<IHackatimeService, MockHackatimeService>();
		services.AddTransient<ReviewViewModel>();

		return services;
	}
}
