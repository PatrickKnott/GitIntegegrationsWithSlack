using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public class GitCredentials
    {
        public string GitToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gitToken"></param>
        /// <param name="userName">Your git user name.</param>
        /// <param name="password">This can be the personal access token or password.</param>
        public GitCredentials(string gitToken,string userName, string password = null)
        {
            GitToken = gitToken;
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">Your git user name.</param>
        /// <param name="password">This can be a personal access token or password.</param>
        public GitCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
