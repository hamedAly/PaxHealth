using PH.Core.Domain;

namespace PH.Services;

public interface ILabResultService
{
    LabResult GetById(int id);
    void InsertLabResult(LabResult labResult);
    void UpdateLabResult(LabResult labResult);
    void DeleteLabResult(LabResult labResult);
    List<LabResult> GetAllLabResults();
}

