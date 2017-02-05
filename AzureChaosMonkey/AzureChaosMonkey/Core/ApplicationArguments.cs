using AzureChaosMonkey.Services;

namespace AzureChaosMonkey.Core
{
    public class ApplicationArguments
    {
        public ApplicationArguments()
        {
            Settings = new Settings();
        }

        public Settings Settings { get; set; }  
    }
}
