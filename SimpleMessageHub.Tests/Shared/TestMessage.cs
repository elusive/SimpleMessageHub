namespace SimpleMessageHub.Tests.Shared
{
    using System;

    public class TestMessage : IMessage
    {
        public TestMessage() : this(string.Empty)
        {
        }

        public TestMessage(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public string Title { get; set; }
        public Guid Id { get; private set; }
    }
}
