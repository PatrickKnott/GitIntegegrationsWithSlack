using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Paramore.Brighter;

namespace GitIntegrationsWithSlack.Commands
{
    public static partial class EnableRequiredReviews
    {
        public class CommandHandler : RequestHandlerAsync<Command>
        {

            private readonly GitHubClient _client;

            public CommandHandler(GitHubClient client)
            {
                _client = client;
            }

            public override async Task<Command> HandleAsync(Command command,
                CancellationToken cancellationToken = new CancellationToken())
            {
                var protectionSettingsUpdate =
                    new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, false));
                await _client.Repository.Branch.UpdateBranchProtection(command.Repository.Id, command.BranchName,
                    protectionSettingsUpdate);
                return await base.HandleAsync(command, cancellationToken);
            }
        }
    }
}
