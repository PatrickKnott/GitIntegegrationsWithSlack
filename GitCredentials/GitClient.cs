using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Paramore.Brighter;
using Paramore.Darker;

namespace GitIntegrationsWithSlack
{
    public class GitClient
    {
        public GitHubClient Client { get; }


        /// <summary>
        /// This is the initial constructor.
        /// </summary>
        /// <param name="gitCredentials">Must have a userName.</param>.
        public GitClient(Credentials credentials, IExceptionLogger logger)
        {
            Client = new GitHubClient(new ProductHeaderValue(credentials.Login));
            Client.Credentials = credentials;
        }

        public async Task<Repository> CreateRepository(string newRepositoryName, string organizationName = "")
        {
            NewRepository newRepository = new NewRepository(newRepositoryName)
            {
                AutoInit = true,
                GitignoreTemplate = "VisualStudio",
                HasIssues = false
            };
            if (organizationName == "")
            {
                return await Client.Repository.Create(newRepository);
            }
            return await Client.Repository.Create(organizationName, newRepository);
        }

        /// <summary>
        /// Creates a new branch in a repository by passing a repository Id.    
        /// </summary>
        /// <param name="repositoryName">The Repository to be modified.</param>
        /// <param name="master">required as heads/""</param>
        /// <param name="develop">required as refs/heads/""</param>
        /// <returns></returns>
        public async Task CreateBranch(string repositoryName, string master = "heads/master", string develop = "refs/heads/develop")
        {
            long repositoryId = GetRepositoryId(repositoryName).GetAwaiter().GetResult();
            var masterBranch = await Client.Git.Reference.Get(repositoryId, master);
            var result = await Client.Git.Reference.Create(repositoryId,
                new NewReference(develop, masterBranch.Object.Sha));
        }

        public async Task<int?> GetTeamId(string organization, string teamName)
        {
            var teams = await Client.Organization.Team.GetAll(organization);
            if (teams.FirstOrDefault(x => x.Name == teamName) == null)
            {
                return null;
            }
            return teams.FirstOrDefault(x => x.Name == teamName).Id;
        }

        public async Task<long> GetRepositoryId(string repositoryName)
        {
            var repositoryId = await Client.Repository.Get(Client.Credentials.Login, repositoryName);
            return repositoryId.Id;
        }

        public async Task UpdateDefaultBranch(string repositoryName, string branchName, long repositoryId)
        {
            var repositoryUpdate = new RepositoryUpdate(repositoryName)
            {
                DefaultBranch = "develop"
            };
            await Client.Repository.Edit(repositoryId, repositoryUpdate);

        }

        /// <summary>
        /// This call Disables force pushes to the branch and prevents it from being deleted.
        /// </summary>
        /// <param name="branchName">The name of the branch to protect.</param>
        /// <param name="repositoryId">The repositoryId, automatically set if the repository was created by this class.</param>
        /// <returns></returns>
        public async Task DisableForcePushesAndPreventDeletion(string branchName, long repositoryId)
        {
            var pushSettingsUpdate =
                new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(false, false));
            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, pushSettingsUpdate);
            await Client.Repository.Branch.RemoveReviewEnforcement(repositoryId, branchName);
        }

        public async void UpdateRequiredReviews(string branchName, long repositoryId)
        {
            var protectionSettingsUpdate =
                new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, false));
            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName,
                protectionSettingsUpdate);
        }

        public async void UpdateRequiredStatusChecks(string branchName, long repositoryId)
        {
            var statusChecksUpdate =
                new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new List<string>()));
            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, statusChecksUpdate);
        }

        /// <summary>
        /// Sets a Team for the repository.  Note! Must be called prior to UpdateBranchProtectionByTeamsWhoCanPushToBranch() for that method to work.
        /// </summary>
        /// <param name="repositoryName">Repository Name</param>
        /// <param name="organization">Usually Retail Success.</param>
        /// <param name="teamName">Usually Bless, Posim, or Retail Success</param>
        /// <returns></returns>
        public async Task AddRepositoryToTeam(string repositoryName, string organization, string teamName = "TestTeam")
        {
            if (repositoryName.Contains('.'))
            {
                teamName = repositoryName.GetSubstringBeforeString(".").ResolveRetailSuccessAsString();
            }
            var id = GetTeamId(organization, teamName);
            if (id.Result == null)
            {
                return;
            }

            await Client.Organization.Team.AddRepository((id.Result ?? default(int)), organization, repositoryName, new RepositoryPermissionRequest(Permission.Push));
        }

        /***************unused below this line. ******************/
        /// <summary>
        /// This call takes a comma separated string of teamNames and sets them as the only ones outside of administrators who can push to a particular branch.  
        /// Note! if the repository(branch) hasn't been added to the team, this method does NOT work, it only marks the branch as protected!  
        /// </summary>
        /// <param name="branchName"></param>
        /// <param name="repositoryId">The repositoryId, this is automatically set from the class, if the class has already gotten the repository and if = -1.</param>
        /// <param name="repositoryName">The repository Name, if it has a . in it, the repository will set the string before the . as the team.</param>
        /// <param name="teamName">The comma separated list of teamNames that will be able to push to the repository, if repositoryName is blank or doesn't have a '.'</param>
        /// <returns></returns>
        public async Task UpdateBranchProtectionByTeamsWhoCanPushToBranch(string branchName, long repositoryId, string repositoryName = "", string teamName = "TestTeam")
        {
            IList<string> teamNames = new List<string>();
            if (repositoryName.Contains('.'))
            {
                teamNames.Add(repositoryName.GetSubstringBeforeString(".").ResolveRetailSuccessAsTeam());
            }
            else
            {
                foreach (var t in teamName.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    teamNames.Add(t.Trim());
                }
            }
            var teams = new BranchProtectionTeamCollection(teamNames);

            var branchProtectionSettingsUpdate =
                new BranchProtectionSettingsUpdate(new BranchProtectionPushRestrictionsUpdate(teams));

            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName,
                branchProtectionSettingsUpdate);
        }

        public async Task<string> GetAllBranches(long repositoryId)
        {
            var branches = await Client.Repository.Branch.GetAll(repositoryId);
            string branchNames = "";
            foreach (var branch in branches)
            {
                branchNames += branch.Name + ", ";
            }
            return branchNames.Remove(branchNames.Length - 2);
        }

    }
}
