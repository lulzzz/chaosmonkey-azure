using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AzureChaosMonkey.Core;
using AzureChaosMonkey.Infrastructure.Logging;
using Fclp;

namespace AzureChaosMonkey
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser<ApplicationArguments>();
            var parsingResult = commandLineParser.Parse(args);
            if (!parsingResult.HasErrors)
            {
                using (var container = Dependencies.Register(commandLineParser.Object))
                {
                    var logger = container.Resolve<IChaosLogger>();
                    logger.Log("Initialization complete!");
                }
            }
            else
            {
                
            }
        }
    }
}
