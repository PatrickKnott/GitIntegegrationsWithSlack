using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public static class SetUpRepositoryCommandHandler
    {
        public static void SetUpNewRepository(string repositoryName, string organizationName = "Retail-Success", string defaultTeamName = "Posim", IExceptionLogger logger = null)
        {
            var credentials = new GitCredentials("PatrickKnott", "3435cd039c521dcf6b8af4a81f492fd542d1c1eb");
            IExceptionLogger thisLogger = new ConsoleLogger();
            if (logger != null)
            {
                thisLogger = logger;
            }
            var client = new GitClient(credentials, thisLogger, "GitIntegrationsWithSlack");
            client.CreateRepo(repositoryName, organizationName).GetAwaiter().GetResult();
            client.UpdateRequiredReviews("master").GetAwaiter();
            client.UpdateRequiredStatusChecks("master").GetAwaiter();
            client.CreateBranch().GetAwaiter().GetResult();
            client.AddRepositoryToTeam(repositoryName, organizationName, defaultTeamName).GetAwaiter().GetResult();
            client.UpdateBranchProtectionByTeamsWhoCanPushToBranch("develop", repositoryName, defaultTeamName).GetAwaiter().GetResult();
            client.UpdateDefaultBranch(repositoryName, "develop").GetAwaiter().GetResult();
        }
    }
}
