using OpsPilot.Domain.Common;
using OpsPilot.Domain.Exceptions;

namespace OpsPilot.Domain.Entities;

/// <summary>
/// An application, App Service or other Azure resource that OpsPilot monitors.
/// </summary>
public class MonitoredApplication : Entity
{
    private MonitoredApplication()
    {
    }

    public MonitoredApplication(
        string name,
        string resourceId,
        string resourceType,
        string subscriptionId,
        string resourceGroup,
        string region,
        string environment,
        string? description = null)
    {
        Name = Require(name, nameof(name));
        ResourceId = Require(resourceId, nameof(resourceId));
        ResourceType = Require(resourceType, nameof(resourceType));
        SubscriptionId = Require(subscriptionId, nameof(subscriptionId));
        ResourceGroup = Require(resourceGroup, nameof(resourceGroup));
        Region = Require(region, nameof(region));
        Environment = Require(environment, nameof(environment));
        Description = description;
        IsMonitoringEnabled = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>Friendly display name.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Full Azure Resource Manager id of the resource.</summary>
    public string ResourceId { get; private set; } = string.Empty;

    /// <summary>Azure resource type, e.g. "Microsoft.Web/sites".</summary>
    public string ResourceType { get; private set; } = string.Empty;

    public string SubscriptionId { get; private set; } = string.Empty;

    public string ResourceGroup { get; private set; } = string.Empty;

    /// <summary>Azure region, e.g. "westeurope".</summary>
    public string Region { get; private set; } = string.Empty;

    /// <summary>Deployment environment, e.g. "Production" or "Staging".</summary>
    public string Environment { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsMonitoringEnabled { get; private set; }

    public string? HealthCheckUrl { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public void EnableMonitoring() => IsMonitoringEnabled = true;

    public void DisableMonitoring() => IsMonitoringEnabled = false;

    private static string Require(string value, string paramName) =>
        string.IsNullOrWhiteSpace(value)
            ? throw new DomainException($"{paramName} must not be empty.")
            : value.Trim();
}
