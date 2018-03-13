using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitIntegrationsWithSlack
{
    public interface IExceptionLogger
    {
        void WriteException(Exception ex, string input);
    }
}
