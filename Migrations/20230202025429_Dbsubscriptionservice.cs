using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace subscription.Migrations
{
    /// <inheritdoc />
    public partial class Dbsubscriptionservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gateway_raw_event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    EventPayload = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gateway_raw_event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment_gateway",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_gateway", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    GatewayTransactionId = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    GatewayOrderId = table.Column<string>(type: "text", nullable: true),
                    GatewayRefCode = table.Column<string>(type: "text", nullable: true),
                    GatewayState = table.Column<string>(type: "text", nullable: true),
                    GatewayRawEventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_gateway_raw_event_GatewayRawEventId",
                        column: x => x.GatewayRawEventId,
                        principalTable: "gateway_raw_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription_plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FeaturedImage = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Gateways = table.Column<Guid>(type: "uuid", nullable: false),
                    CostDisplay = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription_plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscription_plan_payment_gateway_Gateways",
                        column: x => x.Gateways,
                        principalTable: "payment_gateway",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PaymentTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GatewaySubscriptionId = table.Column<string>(type: "text", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscription_subscription_plan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "subscription_plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscription_transaction_PaymentTransactionId",
                        column: x => x.PaymentTransactionId,
                        principalTable: "transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscription_PaymentTransactionId",
                table: "subscription",
                column: "PaymentTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscription_SubscriptionPlanId",
                table: "subscription",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_subscription_plan_Gateways",
                table: "subscription_plan",
                column: "Gateways");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_GatewayRawEventId",
                table: "transaction",
                column: "GatewayRawEventId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "subscription_plan");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "payment_gateway");

            migrationBuilder.DropTable(
                name: "gateway_raw_event");
        }
    }
}
