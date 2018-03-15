using System;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class EnableRequiredReviews
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
