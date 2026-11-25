using OpsPilot.Domain.Common;
using OpsPilot.Domain.Enums;
using OpsPilot.Domain.Exceptions;

namespace OpsPilot.Domain.Entities;

public class Incident : Entity
{
    private Incident()
    {
    }

    public Incident(
        Guid applicationId,
        string title,
        string description,
        IncidentSeverity severity)
    {
        if (applicationId == Guid.Empty)
        {
            throw new DomainException("applicationId must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("title must not be empty.");
        }

        ApplicationId = applicationId;
        Title = title.Trim();
        Description = description?.Trim() ?? string.Empty;
        Severity = severity;
        Status = IncidentStatus.Open;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private readonly List<Investigation> _investigations = [];

    public Guid ApplicationId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public IncidentSeverity Severity { get; private set; }

    public IncidentStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? ResolvedAt { get; private set; }

    /// <summary>The investigations run for this incident (one incident can have many).</summary>
    public IReadOnlyCollection<Investigation> Investigations => _investigations.AsReadOnly();

    public Investigation StartInvestigation()
    {
        var investigation = new Investigation(Id);
        _investigations.Add(investigation);
        return investigation;
    }
}
