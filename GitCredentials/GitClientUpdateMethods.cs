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
                //log exception method.
                throw;
            }
        }

        public async Task<string> DisableForcePushesAndPreventDeletion(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }

            try
            {
                /*
                //TODO try adding the team.
                var pushSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(false, false));
                */
                /**/
                //This causes the protections to allow this team to push to the repo.  It does not overwrite the read, discuss with Adam.
                var teams = new BranchProtectionTeamCollection();
                teams.Add("Bless");
                var pushSettingsUpdate = new BranchProtectionSettingsUpdate(new BranchProtectionPushRestrictionsUpdate(teams));



                await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, pushSettingsUpdate);
                await Client.Repository.Branch.RemoveReviewEnforcement(repositoryId, branchName);

                return $"updating {branchName} successful!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public async Task UpdateRequiredReviews(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }
            var protectionSettingsUpdate =
                new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, false));
            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName,
                protectionSettingsUpdate);
        }

        public async Task UpdateRequiredStatusChecks(string branchName, long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }
            var statusChecksUpdate =
                new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new List<string>()));
            await Client.Repository.Branch.UpdateBranchProtection(repositoryId, branchName, statusChecksUpdate);
        }

        /// <summary>
        /// Sets a Team for the repository.  
        /// </summary>
        /// <param name="repositoryName">Repository Name</param>
        /// <param name="organization">Usually Retail Success.</param>
        /// <param name="teamName">Usually Bless, Posim, or Retail Success</param>
        /// <returns></returns>
        public async Task AddRepositoryToTeam(string repositoryName, string organization, string teamName = "TestTeam")
        {
            if (repositoryName.Contains('.'))
            {
                teamName = repositoryName.GetSubstringBeforeString(".").ResolveRetailSuccess();
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
                //logging
                throw;
            }
        }

    }
}
