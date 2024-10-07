namespace PH.Core.Domain;

public class MedicalDocument : BaseEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }

}