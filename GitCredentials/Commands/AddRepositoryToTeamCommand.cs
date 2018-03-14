using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class AddRepositoryToTeam
    {
        public sealed class Command : Paramore.Brighter.Command
        {
            public string RepositoryName { get; }
            public string Organization { get; }
            public Repository Repository { get; }
            public string TeamName { get; }

            public Command(string repositoryName, string organization, Repository repository, string teamName = "Posim") : base(Guid.NewGuid())
            {
                RepositoryName = repositoryName;
                Organization = organization;
                Repository = repository;
                TeamName = teamName;
            }
        }
    }
}
