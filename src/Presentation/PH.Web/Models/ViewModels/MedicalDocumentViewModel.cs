namespace PH.Web.Models
{
    public class MedicalDocumentViewModel
    {
        public List<DocumentModel> MedicalDocuments { get; set; } = new List<DocumentModel>();
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
