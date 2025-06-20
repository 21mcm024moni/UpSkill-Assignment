using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.OrderProcessors
{
    public class VideoProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) =>
            order.ProductType == ProductType.Video && string.Equals(order.ProductName, "Learning to Ski", StringComparison.OrdinalIgnoreCase);

        public void Process(Order order)
        {
            string basePath = "C:\\work\\Task1\\OrderProcessingSystem\\PackingSlips";
            Directory.CreateDirectory(basePath);

            string filePath = Path.Combine(basePath, $"{order.ProductName}_PackingSlip.txt");
            string bonus = "Bonus: Free 'First Aid' video included.\n";

            if (File.Exists(filePath))
                File.AppendAllText(filePath, bonus);
            else
                File.WriteAllText(filePath, $"Packing Slip\nProduct: {order.ProductName}\n{bonus}");

            Console.WriteLine("Added free 'First Aid' video to packing slip.");
        }
    }
}
