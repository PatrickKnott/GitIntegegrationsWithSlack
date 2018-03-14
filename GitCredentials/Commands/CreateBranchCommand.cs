using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class CreateBranch
    {
        public sealed class Command : Paramore.Brighter.Command
        {
            public Repository Repository { get; }
            public string OriginalBranch { get; }
            public string NewBranch { get; }

            public Command(Repository repository, string originalBranch = "heads/master", string newBranch = "refs/heads/develop") : base( Guid.NewGuid())
            {
                Repository = repository;
                OriginalBranch = originalBranch;
                NewBranch = newBranch;
            }
        }
    }
}
