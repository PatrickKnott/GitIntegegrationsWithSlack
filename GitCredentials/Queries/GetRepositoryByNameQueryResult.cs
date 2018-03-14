using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack.Queries
{
    public static partial class GetRepositoryByName
    {
        public class QueryResult
        {
            public Repository Repository { get; set; }
        }
    }
}
