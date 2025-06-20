
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.OrderProcessors
{
    public class PhysicalProductProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) => order.ProductType == ProductType.PhysicalProduct;

        public void Process(Order order)
        {
            Directory.CreateDirectory("PackingSlips");
            string content = $"Packing Slip\nProduct: {order.ProductName}\nShipping: Standard\n";
            File.WriteAllText($"PackingSlips/{order.ProductName}_PackingSlip.txt", content);
            Console.WriteLine($"Packing slip generated for '{order.ProductName}'.");
        }
    }
}
