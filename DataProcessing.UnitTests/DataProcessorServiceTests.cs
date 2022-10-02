using AutoMapper;
using DataProcessing.Application.Contracts;
using DataProcessing.Application.Exceptions;
using DataProcessing.Application.Models;
using DataProcessing.Application.Services;
using DataProcessing.Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace DataProcessing.UnitTests
{
    [TestFixture]
    public class DataProcessorServiceTests
    {
        private readonly Mock<ITaskProcessor> _taskProcessorMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IDataJobRepository> _repoMock = new();

        [Test]
        public void GetAllDataJobs_CallsDependencies()
        {
            // arrange
            var mockData = GetMockData();
            _repoMock.Setup(r => r.GetAll()).Returns(mockData);

            var service = CreateSut();

            // act
            var result = service.GetAllDataJobs();

            // assert
            _repoMock.Verify(r => r.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<DataJobDto>>(mockData), Times.Once);
        }

        [Test]
        public void GetDataJob_ValidId_CallsMapper()
        {
            // arrange
            var mockData = GetMockData();
            var guid = mockData[0].Id;

            _repoMock.Setup(r => r.Get(guid)).Returns(mockData[0]);

            var service = CreateSut();

            // act
            var result = service.GetDataJob(guid);

            // assert
            _mapperMock.Verify(m => m.Map<DataJobDto>(mockData[0]), Times.Once);
        }

        [Test]
        public void GetDataJob_InvalidId_ThrowsNotFoundException()
        {
            // arrange
            var invalidId = Guid.Empty;
            
            var mockData = GetMockData();
            _repoMock.Setup(r => r.GetAll()).Returns(mockData);

            var service = CreateSut();

            // act
            var action = () => service.GetDataJob(invalidId);

            // assert
            action.Should().Throw<NotFoundException>();
        }

        [Test]
        public void Create_ValidInput_PersistsAndAssignsId()
        {
            // arrange
            var requestDto = new DataJobDto { Links = new List<LinkDto> { new(), new() }};
            var domainObject = new DataJob(Guid.NewGuid(), string.Empty, string.Empty, DataJobStatus.New);
            _mapperMock.Setup(m => m.Map<DataJob>(requestDto)).Returns(domainObject);

            var service = CreateSut();

            // act
            var result = service.Create(requestDto);

            // assert
            result.Id.Should().NotBe(Guid.Empty);
            _mapperMock.Verify(m => m.Map<Link>(It.IsAny<LinkDto>()), Times.Exactly(2), "Because there are two links");
            _repoMock.Verify(r => r.AddOrUpdate(domainObject), Times.Once);
        }

        private static List<DataJob> GetMockData()
        {
            var mockData = new List<DataJob>();

            var fakeItem = new DataJob(Guid.NewGuid(), "test name", "test path", DataJobStatus.New);
            mockData.Add(fakeItem);

            return mockData;
        }

        private DataProcessorService CreateSut()
        {
            return new DataProcessorService(_mapperMock.Object, _repoMock.Object, _taskProcessorMock.Object);
        }
    }
}
