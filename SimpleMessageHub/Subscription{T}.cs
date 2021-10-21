namespace SimpleMessageHub
{
    using System;

    internal class Subscription<TMessage> where TMessage : class, IMessage
    {
        internal Subscription(Action<TMessage> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            Token = Guid.NewGuid();
            Handler = handler;
        }

        public Guid Token { get; set; }
        internal Action<TMessage> Handler { get; set; }
    }
}
