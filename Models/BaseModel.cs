namespace Subscription_service.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}