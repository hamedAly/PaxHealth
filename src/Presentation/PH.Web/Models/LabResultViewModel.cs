namespace PH.Web.Models
{
    public class LabResultViewModel
    {
        public List<ResultModel> LabResults { get; set; } = new List<ResultModel>();
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
