using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.OrderProcessors
{
    public class BookProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) => order.ProductType == ProductType.Book;

        public void Process(Order order)
        {
            string basePath = "C:\\work\\Task1\\OrderProcessingSystem\\PackingSlips";
            Directory.CreateDirectory(basePath); 

            string fileName = $"{order.ProductName}_RoyaltySlip.txt";
            string filePath = Path.Combine(basePath, fileName);

            string content = $"Duplicate Packing Slip\nProduct: {order.ProductName}\nSent to: Royalty Department\n";
            File.WriteAllText(filePath, content);

            Console.WriteLine($"Royalty packing slip generated for '{order.ProductName}' at: {filePath}");
        }
    }
}
