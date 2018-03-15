using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class EnableRequiredStatusChecks
    {

        public sealed class Command : Paramore.Brighter.Command
        {
            public string BranchName { get; }
            public long RepositoryId { get; }

            public Command(long repositoryId, string branchName = "master") : base(Guid.NewGuid())
            {
                BranchName = branchName;
                RepositoryId = repositoryId;
            }
        }
    }
}
