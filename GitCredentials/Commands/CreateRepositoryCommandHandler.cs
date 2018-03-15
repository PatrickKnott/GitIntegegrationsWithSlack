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
    public static partial class CreateRepository
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {
            private readonly GitHubClient _client;

            public CommandHandler(GitHubClientOptions options)
            {
                _client = options.GitHubClientFactory();
            }
            public override async Task<Command> HandleAsync(Command command, CancellationToken cancellationToken = default(CancellationToken))
            {
                NewRepository newRepository = new NewRepository(command.RepositoryName)
                {
                    AutoInit = true,
                    GitignoreTemplate = "VisualStudio",
                    HasIssues = false
                };
                var x = await _client.Repository.Create(command.Organization, newRepository);
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
