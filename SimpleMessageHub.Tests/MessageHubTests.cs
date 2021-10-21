namespace SimpleMessageHub.Tests
{
    using System;

    using Xunit;

    public class MessageHubTests : IDisposable
    {
        private readonly MessageBus _messageHub;

        public MessageHubTests()
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
            var msg1 = new TestMessage { Id = 1, Title = "Title1" };
            var msg2 = new TestMessage { Id = 2, Title = "Title2" };
            _messageHub.Subscribe((TestMessage m) => handledCount++);

            // act
            _messageHub.Publish(msg1);
            _messageHub.Publish(msg2);

            // assert
            Assert.Equal(2, handledCount);
        }


        public class TestMessage : IMessage
        {
            public string Title { get; set; }
            public int Id { get; set; }
        }
    }
}
