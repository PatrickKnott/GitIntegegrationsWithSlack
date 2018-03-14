using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Paramore.Brighter;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class DisableForcePushAndDeletion
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {
            private readonly GitHubClient _client;

            public CommandHandler(GitHubClient client)
            {
                _client = client;
            }

            public override async Task<Command> HandleAsync(Command command,
                CancellationToken cancellationToken = new CancellationToken())
            {
                var pushSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(false, false));
                await _client.Repository.Branch.UpdateBranchProtection(command.Repository.Id, command.BranchName, pushSettingsUpdate);
                await _client.Repository.Branch.RemoveReviewEnforcement(command.Repository.Id, command.BranchName);
                return await base.HandleAsync(command, cancellationToken);
            }

        }
    }
}
