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
    public static partial class CreateBranch
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
                var masterBranch = await _client.Git.Reference.Get(command.RepositoryId, command.OriginalBranch);
                await _client.Git.Reference.Create(command.RepositoryId,
                    new NewReference(command.NewBranch, masterBranch.Object.Sha));
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
