using AzureChaosMonkey.Infrastructure.Logging;
using AzureChaosMonkey.Services.Monkeys;

namespace AzureChaosMonkey.Services.Manager
{
    public class ChaosManager : IChaosManager
    {
        private readonly Settings _settings;
        private readonly IChaosLogger _logger;

        public ChaosManager(Settings settings,
                            IChaosLogger logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void UnleashChaos()
        {
            new VmMonkey(_settings, _logger).Unleash();
        }
    }
}
