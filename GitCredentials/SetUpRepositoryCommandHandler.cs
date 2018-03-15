using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack
{
    public static class SetUpRepositoryCommandHandler
    {
        public static async Task SetUpNewRepository(string repositoryName, string organizationName = "Retail-Success", string defaultTeamName = "Posim", IExceptionLogger logger = null)
        {
            Credentials credentials = new Credentials(GitCredentials.UserName, GitCredentials.Password);
            IExceptionLogger thisLogger = new ConsoleLogger();
            if (logger != null)
            {
                thisLogger = logger;
            }
            var client = new GitClient(credentials, thisLogger);
            var repo = await client.CreateRepository(repositoryName, organizationName);
            client.UpdateRequiredReviews("master", repo.Id);
            client.UpdateRequiredStatusChecks("master", repo.Id);
            await client.CreateBranch(repositoryName);
            await client.AddRepositoryToTeam(repositoryName, organizationName, defaultTeamName);
            await client.UpdateBranchProtectionByTeamsWhoCanPushToBranch("develop", repo.Id, repositoryName, defaultTeamName);
            await client.UpdateDefaultBranch(repositoryName, "develop", repo.Id);
        }
    }
}
