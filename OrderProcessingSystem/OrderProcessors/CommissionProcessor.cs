using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.OrderProcessors
{
    public class CommissionProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) =>
            order.ProductType == ProductType.PhysicalProduct || order.ProductType == ProductType.Book;

        public void Process(Order order)
        {
            Console.WriteLine($"Commission payment generated to the agent {order.Agent}.");
        }
    }
}
