using System;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class EnableRequiredReviews
    {
        public sealed class Command : Paramore.Brighter.Command
        {
            public string BranchName { get; }
            public Repository Repository { get; }

            public Command(string branchName, Repository repository) : base(Guid.NewGuid())
            {
                BranchName = branchName;
                Repository = repository;
            }
        }
    }
}
