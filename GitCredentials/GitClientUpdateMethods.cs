using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public partial class GitClient
    {
        public async Task UpdateDefaultBranch(string repositoryName, string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }
            var repositoryUpdate = new RepositoryUpdate(repositoryName)
            {
                DefaultBranch = "develop"
            };
            try
            {
                await Client.Repository.Edit(repositoryId, repositoryUpdate);
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "UpdateDefaultBranch Failed!");
                throw ex;
            }
        }

        /// <summary>
        /// This call Disables force pushes to the branch and prevents it from being deleted.
        /// </summary>
        /// <param name="branchName">The name of the branch to protect.</param>
        /// <param name="repositoryId">The repositoryId, automatically set if the repository was created by this class.</param>
        /// <returns></returns>
        public async Task DisableForcePushesAndPreventDeletion(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }

            try
            {
                var pushSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(false, false));
                await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, pushSettingsUpdate);
                await Client.Repository.Branch.RemoveReviewEnforcement(repositoryId, branchName);
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "DisableForcePushesAndPreventDeletion Failed!");
                throw ex;
            }
        }

        public async Task UpdateRequiredReviews(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }

            try
            {
                var protectionSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, false));
                await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName,
                    protectionSettingsUpdate);
            }
            catch(Exception ex)
            {
                Logger.WriteException(ex, "UpdateRequiredReviews Failed!");
                throw ex;
            }
        }

        public async Task UpdateRequiredStatusChecks(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }

            try
            {
                var statusChecksUpdate =
                    new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(true, new List<string>()));
                await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, statusChecksUpdate);
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "UpdateRequiredStatusChecks Failed!");
                throw ex;
            }
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

            try
            {
                await Client.Organization.Team.AddRepository((id.Result ?? default(int)), organization, repositoryName, new RepositoryPermissionRequest(Permission.Push));
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "UpdateAddRepositoryToTeam Failed!");
                throw ex;
            }
        }

        /// <summary>
        /// This call takes a comma separated string of teamNames and sets them as the only ones outside of administrators who can push to a particular branch.  
        /// Note! if the repository(branch) hasn't been added to the team, this method does NOT work, it only marks the branch as protected!  
        /// </summary>
        /// <param name="branchName"></param>
        /// <param name="repositoryName">The repository Name, if it has a . in it, the repository will set the string before the . as the team.</param>
        /// <param name="teamName">The comma separated list of teamNames that will be able to push to the repository, if repositoryName is blank or doesn't have a '.'</param>
        /// <param name="repositoryId">The repositoryId, this is automatically set from the class, if the class has already gotten the repository and if = -1.</param>
        /// <returns></returns>
        public async Task UpdateBranchProtectionByTeamsWhoCanPushToBranch(string branchName, string repositoryName = "", string teamName = "TestTeam", long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }
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

            try
            {
                var branchProtectionSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionPushRestrictionsUpdate(teams));

                await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName,
                    branchProtectionSettingsUpdate);
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "UpdateBranchProtectionByTeamsWhoCanPushToBranch Failed!");
                throw ex;
            }
        }

    }
}
