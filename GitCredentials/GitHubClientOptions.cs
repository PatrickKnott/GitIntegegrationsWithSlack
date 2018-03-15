using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack
{
    public sealed class GitHubClientOptions
    {
        public Func<GitHubClient> GitHubClientFactory { get; set; }
        public string OrganizationName { get; set; }
        public string DefaultTeamName { get; set; }
        public string UserName { get; set; }
    }    
}
