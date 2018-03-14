using Paramore.Darker;

namespace GitIntegrationsWithSlack.Queries
{
    public static partial class GetRepositoryById
    {
        public class Query : IQuery<QueryResult>
        {
            public long Id { get; }

            public Query(long id)
            {
                Id = id;
            }
        }
    }
}
