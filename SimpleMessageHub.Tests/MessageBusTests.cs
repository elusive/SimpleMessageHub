namespace SimpleMessageHub.Tests
{
    using SimpleMessageHub.Tests.Shared;

    using System;

    using Xunit;

    public class MessageBusTests : IDisposable
    {
        private readonly MessageBus _messageHub;

        public MessageBusTests()
        {
            // setup
            _messageHub = MessageBus.Default;
        }

        public void Dispose()
        {
            // cleanup
        }

        [Fact]
        public void TestPublishCallsHandlerForAllMessages()
        {
            // arrange
            var handledCount = 0;
            var msg1 = new TestMessage("Title1");
            var msg2 = new TestMessage("Title2");
            _messageHub.Subscribe((TestMessage m) => handledCount++);

            // act
            _messageHub.Publish(msg1);
            _messageHub.Publish(msg2);

            // assert
            Assert.Equal(2, handledCount);
        }
    }
}
