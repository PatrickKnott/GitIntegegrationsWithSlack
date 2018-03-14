using Octokit;

namespace GitIntegrationsWithSlack.Queries
{
    public static partial class GetRepositoryById
    {
        public class QueryResult
        {
            public Repository Repository { get; set; }
        }
    }
}
