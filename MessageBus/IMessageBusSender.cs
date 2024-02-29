namespace MessageBus
{
    public interface IMessageBusSender
    {
        void SendMessage(object message, string queue);
        void SendExchangeMessage(object message, string exchange);
    }
}