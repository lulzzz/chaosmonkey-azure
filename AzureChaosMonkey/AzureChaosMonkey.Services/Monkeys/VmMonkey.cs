using System;
using System.Threading.Tasks;
using AzureChaosMonkey.Infrastructure.Logging;
using Microsoft.Azure.Management.Compute;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace AzureChaosMonkey.Services.Monkeys
{
    public class VmMonkey : BaseMonkey
    {
        public VmMonkey(Settings settings, IChaosLogger logger) : base(settings, logger)
        {
        }

        public override async void Unleash()
        {
            var token = GetAccessTokenAsync();
            var credential = new TokenCredentials(token.Result.AccessToken);
            var computeManagementClient = new ComputeManagementClient(credential)
            { SubscriptionId = Settings.SubscriptionId };
            await computeManagementClient.VirtualMachines.PowerOffAsync(Settings.ResourceGroupName, Settings.VmName);
            Logger.Log($"Stopped the virtual machine [{Settings.VmName}] from the {Settings.ResourceGroupName} Resource Group");
        }
    }
}
