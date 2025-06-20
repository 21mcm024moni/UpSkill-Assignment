using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.Services;

namespace OrderProcessingSystem.OrderProcessors
{
    public class MembershipActivationProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) => order.ProductType == ProductType.Membership;

        public void Process(Order order)
        {
            Console.WriteLine("Membership activated.");
            EmailService.Send(order.CustomerEmail, "Membership Activated", $"Your membership for {order.ProductName} is now active.");
        }
    }
}
