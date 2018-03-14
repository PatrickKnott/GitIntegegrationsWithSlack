using Paramore.Darker;

namespace GitIntegrationsWithSlack.Queries
{
    public static partial class GetRepositoryByName
    {
        public class Query : IQuery<QueryResult>
        {
            public string Owner { get; }
            public string RepositoryName { get; }

            public Query(string owner, string repositoryName)
            {
                Owner = owner;
                RepositoryName = repositoryName;
            }
        }
    }
}
