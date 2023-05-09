namespace SimpleMessageHub
{
    using System;
    using System.Collections.Generic;

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
            Subscription<TMessage>? existingSub = null;
            foreach (var sub in subscriptions)
            {
                if (sub is Subscription<TMessage> s && 
                    s.Handler == eventHandler)
                {
                    existingSub = s;
                    break;
                }
            }

            if (existingSub == null)
            {
                var newSubscription = new Subscription<TMessage>(eventHandler);
                subscriptions.Add(newSubscription);
                returnToken = newSubscription.Token;
                _observers[t] = subscriptions;  // update observers because we added sub
            }
            else
            {
                returnToken = existingSub.Token;
            }

            return returnToken;
        }

        public void Unsubscribe(Guid subscriptionToken)
        {
            foreach(var subList in _observers.Values)
            {                
                var sub = FindSubscriptionByToken(subList, subscriptionToken);
                if (sub != null)
                {
                    subList.Remove(sub);
                    break;
                }
            }
        }


        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var t = message.GetType();
            if (_observers.ContainsKey(t))
            {
                var subscriptions = _observers[t];
                if (subscriptions == null || subscriptions.Count == 0) return;
                foreach (var subscription in subscriptions)
                {
                    //if (dispatch)
                    //{
                        // TODO: Add windows sdk and use Dispatcher to invoke method                        
                    //}

                    ((Subscription<TMessage>)subscription).Handler.Invoke(message);
                }
            }
        }


        Subscription<IMessage>? FindSubscriptionByToken(List<object> lst, Guid token)
        {
            foreach (var o in lst)
            {
                if (o is Subscription<IMessage> sub && sub.Token == token)
                {
                    return sub;
                }
            }

            return null;
        }
    }    
}
