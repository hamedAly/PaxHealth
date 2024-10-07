using PH.Core.Data;
using PH.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PH.Services
{
    public class MedicalDocumentService : IMedicalDoucmentService
    {
        private readonly IRepository<MedicalDocument> _medicalDocumentRepository;

        public MedicalDocumentService(IRepository<MedicalDocument> medicalDocumentRepository)
        {
            _medicalDocumentRepository = medicalDocumentRepository ?? throw new ArgumentNullException(nameof(medicalDocumentRepository));
        }

        public MedicalDocument GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid medical document ID", nameof(id));
            }

            return _medicalDocumentRepository.GetById(id);
        }

        public void InsertMedicalDocument(MedicalDocument medicalDocument)
        {
            if (medicalDocument == null)
            {
                throw new ArgumentNullException(nameof(medicalDocument), "Medical document cannot be null.");
            }

            _medicalDocumentRepository.Insert(medicalDocument);
        }

        public void UpdateMedicalDocument(MedicalDocument medicalDocument)
        {
            if (medicalDocument == null)
            {
                throw new ArgumentNullException(nameof(medicalDocument), "Medical document cannot be null.");
            }

            _medicalDocumentRepository.Update(medicalDocument);
        }

        public void DeleteMedicalDocument(MedicalDocument medicalDocument)
        {
            if (medicalDocument == null)
            {
                throw new ArgumentNullException(nameof(medicalDocument), "Medical document cannot be null.");
            }

            _medicalDocumentRepository.Delete(medicalDocument);
        }

        public List<MedicalDocument> GetAllMedicalDocuments()
        {
            return _medicalDocumentRepository.GetAll().ToList();
        }
    }
}