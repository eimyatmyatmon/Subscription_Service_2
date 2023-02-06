using Microsoft.EntityFrameworkCore;
using Subscription_service.Models;
namespace Subscription_service.Data
{
    public class DataContext : DbContext
    {
        /* public DataContext(DbContextOptions<DataContext> options) : base(options){}*/
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //GateWayRawEvent Property Configuration
            modelBuilder.Entity<GatewayRawEvent>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<GatewayRawEvent>()
                .Property(e => e.OrderId)
                .IsRequired();

            //PaymentGateWay Property Configuration
            modelBuilder.Entity<PaymentGateway>()
            .HasKey(pg => pg.Id);

            modelBuilder.Entity<PaymentGateway>()
                .Property(pg => pg.Platform)
                .IsRequired();

            //Subscription Property Configuration
            modelBuilder.Entity<Subscription>()
            .HasKey(s => s.Id);

            modelBuilder.Entity<Subscription>()
                .Property(s => s.UserId)
                .IsRequired();

            modelBuilder.Entity<Subscription>()
                .Property(s => s.PaymentTransactionId)
                .IsRequired();

            //SubscriptionPlan Property Configuration
            modelBuilder.Entity<SubscriptionPlan>()
            .HasKey(sp => sp.Id);

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(sp => sp.Title)
                .IsRequired();

            //Transaction Property Configuration
            modelBuilder.Entity<Transaction>()
            .HasKey(tr => tr.Id);

            modelBuilder.Entity<Transaction>()
            .Property(tr => tr.Platform)
            .IsRequired();

            //Enum SubscriptionStatus Configuration
            modelBuilder
           .Entity<Subscription>()
           .Property(s => s.Status)
           .HasConversion(
               v => v.ToString(),
               v => (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), v));

            //Enum TransactionStatus Configuration
            modelBuilder
           .Entity<Transaction>()
           .Property(t => t.State)
           .HasConversion(
               u => u.ToString(),
               u => (TransactionState)Enum.Parse(typeof(TransactionState), u));

            //Entity Configuration           
            //one to one (Transaction=>GatewayRawEvent)
            modelBuilder.Entity<Transaction>()
               .HasOne(g => g.GatewayRawEvent)
               .WithOne(t => t.Transaction)
               .HasForeignKey<Transaction>(t => t.GatewayRawEventId);

            //one to one(Subscription=>Transaction)
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Transaction)
                .WithOne(t => t.Subscription)
                .HasForeignKey<Subscription>(t => t.PaymentTransactionId);

            //one to many(SubscriptionPlan=>Subscription)
            modelBuilder.Entity<SubscriptionPlan>()
                .HasMany(s => s.Subscriptions)
                .WithOne(t => t.SubscriptionPlan)
                .HasForeignKey(t => t.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //one to many (PaymentGateway=>SubscriptionPlan)
            modelBuilder.Entity<PaymentGateway>()
                .HasMany(s => s.SubscriptionPlans)
                .WithOne(t => t.PaymentGateway)
                .HasForeignKey(t => t.Gateways)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>().ToTable("subscription");
            modelBuilder.Entity<SubscriptionPlan>().ToTable("subscription_plan");
            modelBuilder.Entity<Transaction>().ToTable("transaction");
            modelBuilder.Entity<GatewayRawEvent>().ToTable("gateway_raw_event");
            modelBuilder.Entity<PaymentGateway>().ToTable("payment_gateway");
        }

        public DbSet<Subscription> Subscriptions { get; set; } = default!;
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;
        public DbSet<GatewayRawEvent> GatewayRawEvents { get; set; } = default!;
        public DbSet<PaymentGateway> PaymentGateways { get; set; } = default!;

    }
}