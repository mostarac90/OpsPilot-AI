using OpsPilot.Domain.Enums;

namespace OpsPilot.Domain.Events;

public sealed record IncidentCreatedEvent(
    Guid IncidentId,
    Guid ApplicationId,
    IncidentSeverity Severity,
    DateTimeOffset OccurredAt);
