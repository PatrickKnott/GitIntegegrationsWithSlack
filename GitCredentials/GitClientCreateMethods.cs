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
        /// <summary>
        /// Creates a repository and stores the repository for later use within the GitClient class instance.
        /// </summary>
        /// <param name="newRepositoryName">The name of the new repository.</param>
        /// <param name="organizationName">The organization name where you want the repository created.</param>
        /// <returns></returns>
        public async Task CreateRepo(string newRepositoryName, string organizationName = "")
        {
            NewRepository newRepository = new NewRepository(newRepositoryName)
            {
                AutoInit = true,
                GitignoreTemplate = "VisualStudio",
                HasIssues = false
            };
            try
            {
                if (organizationName == "")
                {
                    Repository = await Client.Repository.Create(newRepository);
                }
                else
                {
                    Repository = await Client.Repository.Create(organizationName, newRepository);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "CreateRepo Failed!");
            }
        }

        /// <summary>
        /// Creates a new branch in a repository by id.    
        /// </summary>
        /// <param name="repositoryId">This is stored in the class if the class was just used to create a repository.</param>
        /// <param name="master">required as heads/""</param>
        /// <param name="develop">required as refs/heads/""</param>
        /// <returns></returns>
        public async Task CreateBranch(long repositoryId = -1, string master = "heads/master", string develop = "refs/heads/develop")
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }

            try
            { 
                var masterBranch = await Client.Git.Reference.Get(repositoryId, master);
                var result = await Client.Git.Reference.Create(repositoryId,
                    new NewReference(develop, masterBranch.Object.Sha));

            }
            catch (Exception ex)
            {
                Logger.WriteException(ex, "CreateBranch Failed!");
            }
        }

    }
}
