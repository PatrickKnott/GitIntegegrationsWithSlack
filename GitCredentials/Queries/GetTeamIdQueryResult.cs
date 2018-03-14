using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack.Queries
{
    public partial class GetTeamId
    {
        public class QueryResult
        {
            public int? TeamId { get; set; }
        }
    }
}
