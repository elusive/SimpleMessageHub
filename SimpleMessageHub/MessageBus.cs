namespace SimpleMessageHub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Simplified message bus. Lightweight and simple.
    /// Does not use weak references nor does it auto
    /// matically unsubscribe.
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private static MessageBus _default;
        static MessageBus() => _default = new MessageBus();
        public static MessageBus Default => _default;

        private object _locker = new object();

        private Dictionary<Type, List<object>> _observers;

        private MessageBus()
        {
            _observers = new Dictionary<Type, List<object>>();
        }

        public Guid Subscribe<TMessage>(Action<TMessage> eventHandler) where TMessage : class, IMessage
        {
            // use type of message to get list of subscriptions
            Guid returnToken;
            var t = typeof(TMessage);
            var subscriptions = _observers.ContainsKey(t) 
                ? _observers[t] 
                : new List<object>();
            
            // if !already a subscription for this handler then add one
            if (!subscriptions
                .Select(s => s as Subscription<TMessage>)
                .Any(s => s.Handler == eventHandler))
            {
                var newSubscription = new Subscription<TMessage>(eventHandler);
                subscriptions.Add(newSubscription);
                returnToken = newSubscription.Token;
                _observers[t] = subscriptions;  // update observers because we added sub
            }
            else
            {
                returnToken = subscriptions
                    .Select(s => (Subscription<TMessage>)s)
                    .First(s => s.Handler == eventHandler).Token;
            }

            return returnToken;
        }

        public void Unsubscribe(Guid subscriptionToken)
        {
            foreach(var subList in _observers.Values)
            {
                var s = subList
                    .Select(s => s as Subscription<IMessage>)
                    .FirstOrDefault(s => s.Token == subscriptionToken);
                if (s != null)
                {
                    subList.Remove(s);
                    break;
                }
            }
        }

        public void Publish<TMessage>(TMessage message, bool dispatch = false) where TMessage : class, IMessage
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var t = message.GetType();
            if (_observers.ContainsKey(t))
            {
                var subscriptions = _observers[t];
                if (subscriptions == null || !subscriptions.Any()) return;
                foreach (var subscription in subscriptions)
                {
                    if (dispatch)
                    {
                        // TODO: Add windows sdk and use Dispatcher to invoke method
                        
                    }

                    ((Subscription<TMessage>)subscription).Handler.Invoke(message);
                }
            }
        }

        private sealed class Subscription<TMessage> where TMessage : class, IMessage
        {
            internal Subscription(Action<TMessage> handler) 
            {
                if (handler == null) throw new ArgumentNullException(nameof(handler));

                Token = Guid.NewGuid();
                Handler = handler;
            }

            internal Guid Token { get; set; }
            internal Action<TMessage> Handler { get; set; }
        }
    }

    
}
