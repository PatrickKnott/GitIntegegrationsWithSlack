using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramore.Brighter;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class CreateRepository
    {
        public class Command : Paramore.Brighter.Command
        {
            public string RepositoryName { get; }
            public string Organization { get; }

            public Command(string repositoryName, string organization = "Retail-Success") : base( Guid.NewGuid())
            {
                RepositoryName = repositoryName;
                Organization = organization;
            }
        }
    }
}
