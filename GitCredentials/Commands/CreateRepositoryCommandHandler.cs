using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Paramore.Brighter;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class CreateRepository
    {
        public class CommandHandler : RequestHandler<Command>
        {
            private readonly GitHubClient _client;

            public CommandHandler(GitHubClient client)
            {
                _client = client;
            }
            public override Command Handle(Command command)
            {
                NewRepository newRepository = new NewRepository(command.RepositoryName)
                {
                    AutoInit = true,
                    GitignoreTemplate = "VisualStudio",
                    HasIssues = false
                };
                _client.Repository.Create(command.Organization, newRepository);
                return base.Handle(command);
            }
        }
    }
}
