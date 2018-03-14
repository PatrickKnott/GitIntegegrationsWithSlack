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
    public static partial class SetDefaultBranch
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {
            private readonly GitHubClient _client;

            public CommandHandler(GitHubClient client)
            {
                _client = client;
            }
            public override async Task<Command> HandleAsync(Command command, CancellationToken cancellationToken = new CancellationToken())
            {
                var repositoryUpdate = new RepositoryUpdate(command.Repository.Name)
                {
                    DefaultBranch = command.BranchName
                };
                await _client.Repository.Edit(command.Repository.Id, repositoryUpdate);
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
