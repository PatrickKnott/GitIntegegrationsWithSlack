using System;
using Microsoft.Extensions.DependencyInjection;
using RetailSuccess.DependencyInjection;
using RetailSuccess.Paramore.Brighter.DependencyInjection;
using RetailSuccess.Paramore.Darker.DependencyInjection;

namespace GitIntegrationsWithSlack
{
    public static class DependencyInjection
    {
        public static void AddGitHub(this IServiceCollection services, Action<GitHubClientOptions> gitHubClientOptions)
        {
            var darkBuilder = services.AddDarker();
            var brightBuilder = services.AddBrighter();
            services = new ParamoreServiceCollection(services, darkBuilder, brightBuilder);
            services.GetDarker()
                .AddHandlers(typeof(Queries.GetRepositoryByName).Assembly);
            services.GetBrighter()
                .AddHandlers(typeof(Commands.CreateBranch).Assembly)
                .AddAsyncHandlers(typeof(Commands.CreateBranch).Assembly);
            services.AddSingleton<GitProcessor>();
            services.AddSingleton<GitHubClientOptions>((provider) =>
            {
                var options = new GitHubClientOptions();
                gitHubClientOptions?.Invoke(options);
                return options;
            });
        }
    }
}
