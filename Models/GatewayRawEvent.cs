namespace Subscription_service.Models

{
    public class GatewayRawEvent : BaseModel
    {

        public string? OrderId { get; set; }
        public string? EventType { get; set; }
        public string? EventPayload { get; set; }
        public Transaction? Transaction { get; set; }
    }
}