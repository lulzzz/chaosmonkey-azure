using System;
using System.Threading.Tasks;
using AzureChaosMonkey.Infrastructure.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureChaosMonkey.Services.Monkeys
{
    public abstract class BaseMonkey
    {
        protected readonly Settings Settings;
        protected readonly IChaosLogger Logger;

        protected BaseMonkey(Settings settings,
                             IChaosLogger logger)
        {
            Settings = settings;
            Logger = logger;
        }

        public abstract void Unleash();

        protected async Task<AuthenticationResult> GetAccessTokenAsync()
        {
            var cc = new ClientCredential(Settings.ClientId, Settings.ClientSecret);
            var context = new AuthenticationContext($"https://login.windows.net/{Settings.TenantId}");
            var token = await context.AcquireTokenAsync("https://management.azure.com/", cc);
            if (token == null)
            {
                throw new InvalidOperationException("Could not get the token");
            }
            return token;

        }
    }
}
