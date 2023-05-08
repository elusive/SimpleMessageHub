namespace SimpleMessageHub.Messages
{
    /// <summary>
    /// Simple message class that carries a string payload.
    /// </summary>
    public class StringMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringMessage"/> class.
        /// </summary>
        /// <param name="message"></param>
        public StringMessage(string message) : base()
        {
            Message = message;
        }

        /// <summary>
        /// Gets the string message value.
        /// </summary>
        public string Message { get; private set; }
    }
}
