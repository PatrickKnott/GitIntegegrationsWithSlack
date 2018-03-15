using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public static class SetUpRepositoryCommandHandler
    {
        public static async Task SetUpNewRepository(string repositoryName, string organizationName = "Retail-Success", string defaultTeamName = "Posim", IExceptionLogger logger = null)
        {
            var credentials = new GitCredentials("PatrickKnott", "");
            IExceptionLogger thisLogger = new ConsoleLogger();
            if (logger != null)
            {
                thisLogger = logger;
            }
            var client = new GitClient(credentials, thisLogger, "GitIntegrationsWithSlack");
            await client.CreateRepo(repositoryName, organizationName);
            client.UpdateRequiredReviews("master");
            client.UpdateRequiredStatusChecks("master");
            await client.CreateBranch();
            await client.AddRepositoryToTeam(repositoryName, organizationName, defaultTeamName);
            await client.UpdateBranchProtectionByTeamsWhoCanPushToBranch("develop", repositoryName, defaultTeamName);
            await client.UpdateDefaultBranch(repositoryName, "develop");
        }
    }
}
