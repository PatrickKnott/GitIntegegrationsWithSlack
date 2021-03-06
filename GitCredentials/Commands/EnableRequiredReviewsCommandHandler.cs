﻿using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Paramore.Brighter;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class EnableRequiredReviews
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {

            private readonly GitHubClient _client;

            public CommandHandler(GitHubClientOptions options)
            {
                _client = options.GitHubClientFactory();
            }

            public override async Task<Command> HandleAsync(Command command,
                CancellationToken cancellationToken = new CancellationToken())
            {
                var protectionSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, false));
                await _client.Repository.Branch.UpdateBranchProtection(command.RepositoryId, command.BranchName,
                    protectionSettingsUpdate);
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
