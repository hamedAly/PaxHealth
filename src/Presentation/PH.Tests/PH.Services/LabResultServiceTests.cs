using Moq;
using PH.Core.Data;
using PH.Core.Domain;
using PH.Services;
using Xunit;

namespace PH.Services.Tests
{
    public class LabResultServiceTests
    {
        private readonly Mock<IRepository<LabResult>> _mockRepository;
        private readonly LabResultService _service;

        public LabResultServiceTests()
        {
            _mockRepository = new Mock<IRepository<LabResult>>();
            _service = new LabResultService(_mockRepository.Object);
        }

        [Fact]
        public void GetById_ValidId_ReturnsLabResult()
        {
          
            int id = 1;
            var expectedLabResult = new LabResult { Id = id };
            _mockRepository.Setup(r => r.GetById(id)).Returns(expectedLabResult);
            var result = _service.GetById(id);
            Assert.Equal(expectedLabResult, result);
        }

        [Fact]
        public void GetById_InvalidId_ThrowsArgumentException()
        {
            int invalidId = 0;
            Assert.Throws<ArgumentException>(() => _service.GetById(invalidId));
        }

        [Fact]
        public void InsertLabResult_ValidLabResult_CallsRepositoryInsert()
        {
            var labResult = new LabResult();
            _service.InsertLabResult(labResult);
            _mockRepository.Verify(r => r.Insert(labResult), Times.Once);
        }

        [Fact]
        public void InsertLabResult_NullLabResult_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.InsertLabResult(null));
        }

        [Fact]
        public void UpdateLabResult_ValidLabResult_CallsRepositoryUpdate()
        {
            var labResult = new LabResult();
            _service.UpdateLabResult(labResult);
            _mockRepository.Verify(r => r.Update(labResult), Times.Once);
        }

        [Fact]
        public void UpdateLabResult_NullLabResult_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.UpdateLabResult(null));
        }

        [Fact]
        public void DeleteLabResult_ValidLabResult_CallsRepositoryDelete()
        {
            var labResult = new LabResult();
            _service.DeleteLabResult(labResult);
            _mockRepository.Verify(r => r.Delete(labResult), Times.Once);
        }

        [Fact]
        public void DeleteLabResult_NullLabResult_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.DeleteLabResult(null));
        }

        [Fact]
        public void GetAllLabResults_ReturnsAllLabResults()
        {
            var expectedLabResults = new List<LabResult>
            {
                new LabResult { Id = 1 },
                new LabResult { Id = 2 },
                new LabResult { Id = 3 }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(expectedLabResults.AsQueryable());
            var result = _service.GetAllLabResults();
            Assert.Equal(expectedLabResults, result);
            Assert.Equal(expectedLabResults.Count, result.Count);
        }
    }
}