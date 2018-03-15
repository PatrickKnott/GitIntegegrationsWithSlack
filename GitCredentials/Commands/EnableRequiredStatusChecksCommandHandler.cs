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
    public static partial class EnableRequiredStatusChecks
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {
            private readonly GitHubClient _client;
            public CommandHandler(GitHubClientOptions options)
            {
                _client = options.GitHubClientFactory();
            }

            public override async Task<Command> HandleAsync(Command command, CancellationToken cancellationToken = new CancellationToken())
            {
                var statusChecksUpdate =
                    new BranchProtectionSettingsUpdate(
                        new BranchProtectionRequiredStatusChecksUpdate(true, new List<string>()));
                await _client.Repository.Branch.UpdateBranchProtection(command.RepositoryId, command.BranchName, statusChecksUpdate);
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
