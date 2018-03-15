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
            public long RepositoryId { get; }
            public string OriginalBranch { get; }
            public string NewBranch { get; }

            public Command(long repository, string originalBranch = "heads/master", string newBranch = "refs/heads/develop") : base( Guid.NewGuid())
            {
                RepositoryId = repository;
                OriginalBranch = originalBranch;
                NewBranch = newBranch;
            }
        }
    }
}
