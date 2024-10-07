namespace PH.Web.Models
{
    public class MedicalDataViewModel
    {
        public IEnumerable<ResultModel> ExamResults { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
