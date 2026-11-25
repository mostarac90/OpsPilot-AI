namespace OpsPilot.Domain.Enums;

public enum EvidenceType
{
    Log,
    Metric,
    Trace,
    Exception,
    DbQuery,
    Alert,
    Deployment,
    ConfigChange,
    Other
}
