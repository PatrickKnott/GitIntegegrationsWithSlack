using GitIntegrationsWithSlack;
using System;

namespace consoleTestGitClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            //SetUpRepositoryCommandHandler.SetUpNewRepository("Bless.NewRepo70", "CoolTestOrganization", "Retail Success");
            SetUpRepositoryCommandHandler.SetUpNewRepository("Bless.GitApiTest", "Retail-Success", "Bless");
            Console.ReadKey();
        }
    }
}
