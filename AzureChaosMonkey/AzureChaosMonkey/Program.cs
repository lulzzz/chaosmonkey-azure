using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AzureChaosMonkey.Core;
using AzureChaosMonkey.Infrastructure.Logging;
using AzureChaosMonkey.Services.Manager;
using Fclp;

namespace AzureChaosMonkey
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser<ApplicationArguments>();

            commandLineParser.Setup(arg => arg.Settings.LogFileName)
                .As('l', "logFileName")
                .WithDescription("Specifies the log filename that will be used to output the execution messages");

            // sets up the parser to execute the callback when -? or --help is detected
            commandLineParser.SetupHelp("?", "help")
                             .Callback(text => Console.WriteLine(text));

            var parsingResult = commandLineParser.Parse(args);
            if (parsingResult.HelpCalled)
            {
                return;
            }

            if (!parsingResult.HasErrors)
            {
                using (var container = Dependencies.Register(commandLineParser.Object))
                {
                    var logger = container.Resolve<IChaosLogger>();
                    logger.Log("Initialization complete!");
                    var chaosManager = container.Resolve<IChaosManager>();
                    chaosManager.UnleashChaos();
                }
            }
            else
            {
                Console.WriteLine(parsingResult.ErrorText);
                Console.ReadKey();
            }
        }
    }
}
