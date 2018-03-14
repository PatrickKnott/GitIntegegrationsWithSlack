using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Paramore.Darker;

namespace GitIntegrationsWithSlack.Queries
{
    public partial class GetTeamId
    {
        public class QueryHandler : QueryHandlerAsync<Query, QueryResult>
        {
            private readonly GitHubClient _client;

            public QueryHandler(GitHubClient client)
            {
                _client = client;
            }

            public override async Task<QueryResult> ExecuteAsync(Query query, CancellationToken cancellationToken = default(CancellationToken))
            {
                var teams = await _client.Organization.Team.GetAll(query.Organization);
                //TODO delete this guard clause so it blows up?
                if (teams.FirstOrDefault(x => x.Name == query.TeamName) == null)
                {
                    return new QueryResult() { TeamId = null };
                }
                return new QueryResult(){TeamId = teams.FirstOrDefault(x => x.Name == query.TeamName).Id};
            }
        }
    }
}
