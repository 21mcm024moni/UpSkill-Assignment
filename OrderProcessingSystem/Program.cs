using Microsoft.Extensions.DependencyInjection;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Services;

class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IOrderProcessingStep, PhysicalProductProcessor>()
            .AddSingleton<IOrderProcessingStep, BookProcessor>()
            .AddSingleton<IOrderProcessingStep, CommissionProcessor>()
            .AddSingleton<IOrderProcessingStep, MembershipActivationProcessor>()
            .AddSingleton<IOrderProcessingStep, MembershipUpgradeProcessor>()
            .AddSingleton<IOrderProcessingStep, VideoProcessor>()
            .AddSingleton<OrderProcessor>()
            .BuildServiceProvider();

        var processor = serviceProvider.GetRequiredService<OrderProcessor>();

        Console.WriteLine("\n Welcome to the Order Processing System\n");

        while (true)
        {
            var order = new Order();

            Console.Write("Enter Product Type (PhysicalProduct, Book, Membership, MembershipUpgrade, Video): ");
            var productTypeInput = Console.ReadLine();

            if (!Enum.TryParse(productTypeInput, true, out ProductType productType))
            {
                Console.WriteLine("Invalid product type. Try again.");
                continue;
            }

            order.ProductType = productType;

            Console.Write("Enter Product Name: ");
            order.ProductName = Console.ReadLine();

            Console.Write("Enter Customer Email: ");
            order.CustomerEmail = Console.ReadLine();

            Console.Write("Enter Order Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                order.Amount = amount;
            }
            else
            {
                Console.WriteLine("Invalid amount. Please try again.\n");
                continue;
            }

            Console.WriteLine("\n Processing your order...\n");

            processor.Process(order);

            Console.WriteLine("\n Order Processed Successfully!");
            Console.WriteLine("\n Do you want to process another order? (yes/no): ");
            var cont = Console.ReadLine();
            if (!string.Equals(cont, "yes", StringComparison.OrdinalIgnoreCase))
                break;
        }
    }
}
