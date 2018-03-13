using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitIntegrationsWithSlack
{
    public partial class GitClient
    {
        private GitHubClient _client;

        public IExceptionLogger Logger;

        public string ProductHeaderValue { get; set; }

        public Repository Repository { get; set; }

        public Uri GitHubEnterprise { get; set; }

        public GitHubClient Client
        {
            get { return _client; }
        }
        /// <summary>
        /// This is the initial constructor.
        /// </summary>
        /// <param name="gitCredentials">Must have a userName.</param>
        /// <param name="logger">Outputs the exception if there is an issue with a task.</param>
        /// <param name="productHeaderValue">must be GitHub username or the name of the application, sets internally if null.</param>
        /// <param name="gitHubEnterpriseUri">This is for an Enterprise, if one is ever used.</param>
        public GitClient(GitCredentials gitCredentials, IExceptionLogger logger, string productHeaderValue = null, string gitHubEnterpriseUri = null)
        {
            ProductHeaderValue = gitCredentials.UserName;
            if (!String.IsNullOrWhiteSpace(productHeaderValue))
            {
                ProductHeaderValue = productHeaderValue;
            }
            if (!String.IsNullOrWhiteSpace(gitHubEnterpriseUri))
            {
                GitHubEnterprise = new Uri(gitHubEnterpriseUri);
                _client = new GitHubClient(new ProductHeaderValue(ProductHeaderValue), GitHubEnterprise);
            }
            else
            {
                _client = new GitHubClient(new ProductHeaderValue(ProductHeaderValue));
            }
            Credentials credentials;
            if (!String.IsNullOrWhiteSpace(gitCredentials.GitToken))
            {
                credentials = new Credentials(gitCredentials.GitToken);
            }
            else
            {
                credentials = new Credentials(gitCredentials.UserName, gitCredentials.Password);
            }

            Logger = logger;
            _client.Credentials = credentials;
        }

    }
}
