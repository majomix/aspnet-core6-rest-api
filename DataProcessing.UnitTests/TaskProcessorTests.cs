using DataProcessing.Application.Services;
using DataProcessing.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace DataProcessing.UnitTests
{
    [TestFixture]
    public class TaskProcessorTests
    {
        private class ImmediateTaskProcessor : TaskProcessor
        {
            protected override Task Delay()
            {
                return Task.FromResult(0);
            }
        }

        [Test]
        public void Process_DataJobIsExecuted_IsProcessingReturnsTrue()
        {
            // arrange
            var processor = new TaskProcessor();
            var dataJob = new DataJob(Guid.NewGuid(), string.Empty, string.Empty, DataJobStatus.New);

            // act
            var notAwaited = processor.Process(dataJob);
            var isProcessing = processor.IsProcessing(dataJob);

            // assert
            dataJob.Status.Should().Be(DataJobStatus.Processing);
            isProcessing.Should().BeTrue();
        }

        [Test]
        public async Task Process_DataJobIsWaitedFor_IsProcessingReturnsFalse()
        {
            // arrange
            var processor = new ImmediateTaskProcessor();
            var dataJob = new DataJob(Guid.NewGuid(), string.Empty, string.Empty, DataJobStatus.New);

            // act
            await processor.Process(dataJob);
            var isProcessing = processor.IsProcessing(dataJob);

            // assert
            dataJob.Status.Should().Be(DataJobStatus.Completed);
            dataJob.Results.Count().Should().Be(2);
            isProcessing.Should().BeFalse();
        }
    }
}
