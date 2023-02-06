using System.ComponentModel.DataAnnotations;
namespace Subscription_service.Models;

public class PaymentGateway : BaseModel
{

    public PaymentGateway()
    {
        SubscriptionPlans = new HashSet<SubscriptionPlan>();
    }
    [Required]
    public string? Platform { get; set; }
    public bool Active { get; set; } = false;

    public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
}