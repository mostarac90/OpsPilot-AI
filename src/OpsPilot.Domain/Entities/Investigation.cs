using OpsPilot.Domain.Common;
using OpsPilot.Domain.Enums;
using OpsPilot.Domain.Exceptions;

namespace OpsPilot.Domain.Entities;

public class Investigation : Entity
{
    private Investigation()
    {
    }

    public Investigation(Guid incidentId)
    {
        if (incidentId == Guid.Empty)
        {
            throw new DomainException("incidentId must not be empty.");
        }

        IncidentId = incidentId;
        Status = InvestigationStatus.Pending;
    }

    /// <summary>The incident this investigation belongs to.</summary>
    public Guid IncidentId { get; private set; }

    public InvestigationStatus Status { get; private set; }

    public string FailureReason { get; private set; } = string.Empty;

    public DateTime StartedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; private set; }

    public string InvestigationSummary { get; private set; } = string.Empty;


    // Start investigation transitions the status from Pending to InProgress.
    public void StartInvestigation()
    {
        EnsureStatus(InvestigationStatus.Pending);
        StartedAt = DateTime.UtcNow;
        Status = InvestigationStatus.InProgress;
    }

    // Complete investigation transitions the status from InProgress to Completed.
    public void CompleteInvestigation(string summary)
    {

        //Check that summary is not null or empty
        if(string.IsNullOrWhiteSpace(summary))
        {
            throw new DomainException("summary must not be empty.");
        }

        EnsureStatus(InvestigationStatus.InProgress);
        Status = InvestigationStatus.Completed;
        InvestigationSummary = summary; 
        CompletedAt = DateTime.UtcNow;
    }

    public void FailInvestigation(string failureReason)
    {
        EnsureStatus(InvestigationStatus.InProgress);
        FailureReason = failureReason;
        CompletedAt = DateTime.UtcNow;

        Status = InvestigationStatus.Failed;
    }

    // Ensures that the Investigation is in the expected status before allowing a transition to a new status.
    private void EnsureStatus(InvestigationStatus expected)
    {
        if (Status != expected)
        {
            throw new DomainException(
                $"Expected status {expected}, but current status is {Status}.");
        }
    }
}
