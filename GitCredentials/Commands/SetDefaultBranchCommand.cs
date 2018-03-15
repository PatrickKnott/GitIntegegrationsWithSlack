using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class SetDefaultBranch
    {
        public sealed class Command : Paramore.Brighter.Command
        {
            public Repository Repository { get; }
            public string BranchName { get; }

            public Command(Repository repository, string branchName = "develop") : base( Guid.NewGuid())
            {
                Repository = repository;
                BranchName = branchName;
            }
        }
    }
}
