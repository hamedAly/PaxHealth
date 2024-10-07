using PH.Core.Data;
using PH.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PH.Services
{
    public class LabResultService : ILabResultService
    {
        private readonly IRepository<LabResult> _labResultRepository;

        public LabResultService(IRepository<LabResult> labResultRepository)
        {
            _labResultRepository = labResultRepository ?? throw new ArgumentNullException(nameof(labResultRepository));
        }

        public LabResult GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid lab result ID", nameof(id));
            }

            return _labResultRepository.GetById(id);
        }

        public void InsertLabResult(LabResult labResult)
        {
            if (labResult == null)
            {
                throw new ArgumentNullException(nameof(labResult), "Lab result cannot be null.");
            }

            _labResultRepository.Insert(labResult);
        }

        public void UpdateLabResult(LabResult labResult)
        {
            if (labResult == null)
            {
                throw new ArgumentNullException(nameof(labResult), "Lab result cannot be null.");
            }

            _labResultRepository.Update(labResult);
        }

        public void DeleteLabResult(LabResult labResult)
        {
            if (labResult == null)
            {
                throw new ArgumentNullException(nameof(labResult), "Lab result cannot be null.");
            }

            _labResultRepository.Delete(labResult);
        }

        public List<LabResult> GetAllLabResults()
        {
            return _labResultRepository.GetAll().ToList();
        }
    }
}