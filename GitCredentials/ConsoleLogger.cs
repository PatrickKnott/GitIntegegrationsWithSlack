using System;

namespace GitIntegrationsWithSlack
{
    public class ConsoleLogger : IExceptionLogger
    {
        public void WriteException(Exception ex, string input)
        {
            Console.WriteLine(input);
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            Console.WriteLine();
        }
    }
}
