using System.Linq;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public partial class GitClient
    {
        public async Task<int?> GetTeamId(string organization, string teamName)
        {
            var teams = await Client.Organization.Team.GetAll(organization);
            if (teams.FirstOrDefault(x => x.Name == teamName) == null)
            {
                return null;
            }
            return teams.FirstOrDefault(x => x.Name == teamName).Id;
        }

        public async Task<long> GetRespositoryId(string userName, string repositoryName)
        {
            var repositoryId = await Client.Repository.Get(userName, repositoryName);
            return repositoryId.Id;
        }


        public async Task<string> GetAllBranches(long repositoryId = -1)
        {
            if (repositoryId == -1)
            {
                repositoryId = Repository.Id;
            }
            var branches = await _client.Repository.Branch.GetAll(repositoryId);
            string branchNames = "";
            foreach (var branch in branches)
            {
                branchNames += branch.Name + ", ";
            }
            return branchNames.Remove(branchNames.Length - 2);
        }





    }
}
