using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GitIntegrationsWithSlack.Queries;
using Octokit;
using Paramore.Brighter;
using Paramore.Darker;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class AddRepositoryToTeam
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {
            private readonly GitHubClient _client;
            private readonly IQueryProcessor _queryProcessor;

            public CommandHandler(GitHubClient client, IQueryProcessor queryProcessor)
            {
                _client = client;
                _queryProcessor = queryProcessor;
            }

            public override async Task<Command> HandleAsync(Command command, CancellationToken cancellationToken = new CancellationToken())
            {
                string teamName = command.TeamName;
                if (command.RepositoryName.Contains('.'))
                {
                    teamName = command.RepositoryName.GetSubstringBeforeString(".").ResolveRetailSuccessAsString();
                }

                var teamId = await _queryProcessor.ExecuteAsync(new GetTeamId.Query(command.Organization, teamName), cancellationToken);

                await _client.Organization.Team.AddRepository((teamId.TeamId ?? default(int)), command.Organization, command.RepositoryName, new RepositoryPermissionRequest(Permission.Push));

                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
