namespace OpsPilot.Domain.Events;

public sealed record IncidentResolvedEvent(
    Guid IncidentId,
    Guid ApplicationId,
    DateTimeOffset ResolvedAt);
