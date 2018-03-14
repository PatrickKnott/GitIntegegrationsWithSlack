using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class DisableForcePushAndDeletion
    {
        public sealed class Command : Paramore.Brighter.Command
        {
            public string BranchName { get; }
            public Repository Repository { get; }
            public Command(string branchName, Repository repository) : base( Guid.NewGuid())
            {
                BranchName = branchName;
                Repository = repository;
            }
        }
    }
}
