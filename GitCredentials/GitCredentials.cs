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

        public GitCredentials(string gitToken,string userName, string password = null)
        {
            GitToken = gitToken;
            UserName = userName;
            Password = password;
        }

        public GitCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
