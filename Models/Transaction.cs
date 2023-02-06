namespace Subscription_service.Models
{
    public enum TransactionState
    {
        Pending, Partial, Success, Fail
    }

    public class Transaction : BaseModel
    {
        public string? Platform { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? GatewayTransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionState State { get; set; }
        public string? ErrorMessage { get; set; }
        public string? GatewayOrderId { get; set; }
        public string? GatewayRefCode { get; set; }
        public string? GatewayState { get; set; }
        public Guid GatewayRawEventId { get; set; } = default!;
        public Subscription? Subscription { get; set; }
        public GatewayRawEvent? GatewayRawEvent { get; set; }
    }
}