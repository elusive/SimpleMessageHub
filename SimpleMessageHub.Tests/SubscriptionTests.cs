namespace SimpleMessageHub.Tests
{
    using SimpleMessageHub.Tests.Shared;

    using System;

    using Xunit;

    public class SubscriptionTests : IDisposable
    {
        public SubscriptionTests()
        { }

        /// <summary>
        /// Cleanup disposable resources.
        /// </summary>
        public void Dispose() { }

        [Fact]
        public void TestSubscriptionHasTokenWhenConstructed()
        {
            // arrange
            Action<TestMessage> handler = (msg) => { };

            // act
            var sub = new Subscription<TestMessage>(handler);

            // assert
            Assert.NotEqual(Guid.Empty, sub.Token);
        }

        [Fact]
        public void TestSubscriptionAcceptsTypedHandler()
        {
            // arrange
            Action<TestMessage> handler = (msg) => { };

            // act
            var sub = new Subscription<TestMessage>(handler);

            // assert
            Assert.IsType<Action<TestMessage>>(sub.Handler);
        }

        [Fact]
        public void TestSubscriptionThrowsArgumentExceptionWhenHandlerIsNull()
        {
            // arrange
            Action<TestMessage> handler = null;

            // act
            var act = () =>
            {
                var sub = new Subscription<TestMessage>(handler);
            };

            // assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
   
}
