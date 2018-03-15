using GitIntegrationsWithSlack.Commands;
using GitIntegrationsWithSlack.Queries;
using Paramore.Brighter;
using Paramore.Darker;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public class GitProcessor
    {

        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;
        private readonly GitHubClientOptions _gitHubClientOptions;

        public GitProcessor(IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor, GitHubClientOptions gitHubClientOptions)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
            _gitHubClientOptions = gitHubClientOptions;
        }

        public async Task CreateAndSetUpRepository(string repositoryName)
        {
            await _commandProcessor.SendAsync(new CreateRepository.Command(repositoryName, _gitHubClientOptions.OrganizationName));
            var repositoryQuery = await _queryProcessor.ExecuteAsync(new GetRepositoryByName.Query(_gitHubClientOptions.OrganizationName, repositoryName));
            var repositoryId = repositoryQuery.Repository.Id;
            await _commandProcessor.SendAsync(new EnableRequiredReviews.Command(repositoryId));
            await _commandProcessor.SendAsync(new EnableRequiredStatusChecks.Command(repositoryId));
            await _commandProcessor.SendAsync(new CreateBranch.Command(repositoryId));
            await _commandProcessor.SendAsync(new AddRepositoryToTeam.Command(repositoryName, _gitHubClientOptions.OrganizationName, _gitHubClientOptions.DefaultTeamName));
            await _commandProcessor.SendAsync(new DisableForcePushAndDeletion.Command(repositoryId));
            await _commandProcessor.SendAsync(new SetDefaultBranch.Command(repositoryQuery.Repository));
        }
    }
}
