using GitIntegrationsWithSlack;
using System;

namespace consoleTestGitClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = new GitCredentials("PatrickKnott", "Job$1979");
            var client = new GitClient(credentials, "PatrickKnott");
            string repoName = "GithubRepoWithOctokit";
            client.CreateRepo(repoName).GetAwaiter().GetResult();
            client.UpdateRequiredReviews("master").GetAwaiter();
            client.UpdateRequiredStatusChecks("master").GetAwaiter();
            client.CreateBranch().GetAwaiter().GetResult();
            var success4 = client.DisableForcePushesAndPreventDeletion("develop").GetAwaiter().GetResult();

            //client.AddRepositoryToTeam(repoName, "CoolTestOrganization", "TestTeam").GetAwaiter().GetResult();
            client.UpdateDefaultBranch(repoName, "develop").GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }
}
