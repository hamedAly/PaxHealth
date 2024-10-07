namespace PH.Core.Domain;

public class LabResult : BaseEntity
{
    public string TestName { get; set; }
    public string ResultValue { get; set; }
    public DateTime ResultDate { get; set; }
    public string Unit { get; set; }
}
