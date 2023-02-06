using System.ComponentModel.DataAnnotations;

namespace Subscription_service.Models
{
    public enum SubscriptionStatus
    {
        Pending, Active, Inactive
    }

    public class Subscription : BaseModel
    {
        [Required]
        public string? UserId { get; set; }
        public Guid PaymentTransactionId { get; set; } = default!;
        public SubscriptionStatus Status { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string? GatewaySubscriptionId { get; set; }
        public string? Currency { get; set; }
        public float Amount { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public Transaction? Transaction { get; set; }
        public SubscriptionPlan? SubscriptionPlan { get; set; }

    }
}