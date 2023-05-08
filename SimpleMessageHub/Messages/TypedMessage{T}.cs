namespace SimpleMessageHub.Messages
{
    public class TypedMessage<T> : MessageBase
    {
        public TypedMessage(T payload) : base()
        {
            Payload = payload;
        }

        public T Payload { get; private set; }
    }
}
