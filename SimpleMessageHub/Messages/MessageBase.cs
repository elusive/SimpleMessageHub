namespace SimpleMessageHub.Messages
{
    using System;

    /// <summary>
    /// Base class for messages that includes built in implementation 
    /// of the <see cref="IMessage"/> interface to prevent need for all
    /// message classes to have to implement the same thing.
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Id value for the message as a guid.
        /// </summary>
        public Guid Id { get; private set; }
    }
}
