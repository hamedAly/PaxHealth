using Microsoft.EntityFrameworkCore;
using Moq;
using PH.Core;
using PH.Core.Data;
using PH.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PH.Data.Tests
{
    public class EntityRepositoryTests
    {
        private Mock<IDbContext> _mockContext;
        private Mock<DbSet<TestEntity>> _mockSet;
        private EntityRepository<TestEntity> _repository;

        public EntityRepositoryTests()
        {
            _mockContext = new Mock<IDbContext>();
            _mockSet = new Mock<DbSet<TestEntity>>();
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockSet.Object);
            _repository = new EntityRepository<TestEntity>(_mockContext.Object);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsEntity()
        {
            var entity = new TestEntity { Id = 1 };
            _mockSet.Setup(s => s.Find(It.IsAny<object>())).Returns(entity);

            var result = _repository.GetById(1);

            Assert.Equal(entity, result);
        }

        [Fact]
        public void Insert_ValidEntity_AddsToContextAndSaves()
        {
            var entity = new TestEntity();

            _repository.Insert(entity);

            _mockSet.Verify(s => s.Add(entity), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Insert_NullEntity_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Insert(null));
        }

        [Fact]
        public void Update_ValidEntity_SavesChanges()
        {
            var entity = new TestEntity();

            _repository.Update(entity);

            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_NullEntity_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Update(null));
        }

        [Fact]
        public void Delete_ValidEntity_RemovesFromContextAndSaves()
        {
            var entity = new TestEntity();

            _repository.Delete(entity);

            _mockSet.Verify(s => s.Remove(entity), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_NullEntity_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Delete(null));
        }
    }

    public class TestEntity : BaseEntity
    {
        public int Id { get; set; }
    }
}