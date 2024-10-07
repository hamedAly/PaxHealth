using PH.Core.Domain;

namespace PH.Services;

public interface IMedicalDoucmentService
{
    MedicalDocument GetById(int id);
    void InsertMedicalDocument(MedicalDocument medicalDocument);
    void UpdateMedicalDocument(MedicalDocument medicalDocument);
    void DeleteMedicalDocument(MedicalDocument medicalDocument);
    List<MedicalDocument> GetAllMedicalDocuments();
}