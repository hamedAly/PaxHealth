namespace PH.Web;

public class ResultModel
{
    public int Id { get; set; }
    public string? TestName { get; set; }
    public string? ResultValue { get; set; }
    public string? Unit { get; set; }
    public DateTime ResultDate { get; set; }
}