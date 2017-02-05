using System;

namespace AzureChaosMonkey.Infrastructure.TimeProvider
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}
