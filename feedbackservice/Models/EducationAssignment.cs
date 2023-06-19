namespace Models;

public class EducationAssignment
{
    public Guid ContentId { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public DateTimeOffset AssignmentTimestamp { get; set; }
}