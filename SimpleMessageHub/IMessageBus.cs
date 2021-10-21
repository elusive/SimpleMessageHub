namespace SimpleMessageHub
{
    using System;

    public interface IMessageBus
    {
        Guid Subscribe<TMessage>(Action<TMessage> eventHandler) where TMessage : class, IMessage;
        void Unsubscribe(Guid subscriptionToken);
        void Publish<TMessage>(TMessage message, bool dispatch = false) where TMessage : class, IMessage;
    }
}
