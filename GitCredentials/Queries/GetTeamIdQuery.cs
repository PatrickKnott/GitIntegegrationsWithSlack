using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramore.Darker;

namespace GitIntegrationsWithSlack.Queries
{
    public partial class GetTeamId
    {
        public class Query : IQuery<QueryResult>
        {
            public string Organization { get; }
            public string TeamName { get; }

            public Query(string organization, string teamName)
            {
                Organization = organization;
                TeamName = teamName;
            }


        }
    }
}
