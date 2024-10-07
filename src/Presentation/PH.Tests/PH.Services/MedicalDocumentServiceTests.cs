using Moq;
using PH.Core.Data;
using PH.Core.Domain;
using Xunit;
namespace PH.Services.Tests
{
    public class MedicalDocumentServiceTests
    {
        private readonly Mock<IRepository<MedicalDocument>> _mockRepository;
        private readonly MedicalDocumentService _service;
        public MedicalDocumentServiceTests()
        {
            _mockRepository = new Mock<IRepository<MedicalDocument>>();
            _service = new MedicalDocumentService(_mockRepository.Object);
        }
        [Fact]
        public void GetById_ValidId_ReturnsMedicalDocument()
        {
            int id = 1;
            var expectedDocument = new MedicalDocument { Id = id };
            _mockRepository.Setup(r => r.GetById(id)).Returns(expectedDocument);
            var result = _service.GetById(id);
            Assert.Equal(expectedDocument, result);
        }
        [Fact]
        public void GetById_InvalidId_ThrowsArgumentException()
        {
            int invalidId = 0;
            Assert.Throws<ArgumentException>(() => _service.GetById(invalidId));
        }
        [Fact]
        public void InsertMedicalDocument_ValidDocument_CallsRepositoryInsert()
        {
            var document = new MedicalDocument();
            _service.InsertMedicalDocument(document);
            _mockRepository.Verify(r => r.Insert(document), Times.Once);
        }
        [Fact]
        public void InsertMedicalDocument_NullDocument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.InsertMedicalDocument(null));
        }
        [Fact]
        public void UpdateMedicalDocument_ValidDocument_CallsRepositoryUpdate()
        {
            var document = new MedicalDocument();
            _service.UpdateMedicalDocument(document);
            _mockRepository.Verify(r => r.Update(document), Times.Once);
        }
        [Fact]
        public void UpdateMedicalDocument_NullDocument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.UpdateMedicalDocument(null));
        }
        [Fact]
        public void DeleteMedicalDocument_ValidDocument_CallsRepositoryDelete()
        {
            var document = new MedicalDocument();
            _service.DeleteMedicalDocument(document);
            _mockRepository.Verify(r => r.Delete(document), Times.Once);
        }
        [Fact]
        public void DeleteMedicalDocument_NullDocument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.DeleteMedicalDocument(null));
        }
        [Fact]
        public void GetAllMedicalDocuments_ReturnsAllDocuments()
        {
            var expectedDocuments = new List<MedicalDocument>
            {
                new MedicalDocument { Id = 1 },
                new MedicalDocument { Id = 2 },
                new MedicalDocument { Id = 3 }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(expectedDocuments.AsQueryable());
            var result = _service.GetAllMedicalDocuments();
            Assert.Equal(expectedDocuments, result);
            Assert.Equal(expectedDocuments.Count, result.Count);
        }
    }
}