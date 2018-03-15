using GitIntegrationsWithSlack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Octokit;
using Paramore.Brighter;
using RetailSuccess.DependencyInjection;
using RetailSuccess.Paramore.Brighter.DependencyInjection;
using RetailSuccess.Paramore.Darker.DependencyInjection;

namespace consoleTestGitClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //DON"T CALL AGAIN
                //SetUpRepository.Run("Bless.GitApiTest2").GetAwaiter().GetResult();
                SetUpRepository.Run("RetailSuccess.NewRepo08", "CoolTestOrganization", "Retail Success").GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}