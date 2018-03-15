using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Paramore.Darker;

namespace GitIntegrationsWithSlack.Queries
{
    public static partial class GetRepositoryByName
    {
        public class QueryHandler : QueryHandlerAsync<Query, QueryResult>
        {
            private readonly GitHubClient _client;

            public override async Task<QueryResult> ExecuteAsync(Query query, CancellationToken cancellationToken = new CancellationToken())
            {
                var repository = await _client.Repository.Get(query.Owner, query.RepositoryName);
                return new QueryResult() { Repository = repository };
            }

            public QueryHandler(GitHubClientOptions options)
            {
                _client = options.GitHubClientFactory();
            }
        }
    }
}
