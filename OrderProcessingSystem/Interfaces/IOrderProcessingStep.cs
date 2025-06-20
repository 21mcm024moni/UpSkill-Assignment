using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Interfaces
{
    public interface IOrderProcessingStep
    {
        bool ShouldProcess(Order order);
        void Process(Order order);
    }
}
