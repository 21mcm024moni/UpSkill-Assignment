using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Services
{
    public class OrderProcessor
    {
        private readonly IEnumerable<IOrderProcessingStep> _steps;

        public OrderProcessor(IEnumerable<IOrderProcessingStep> rules)
        {
            _steps = rules;
        }

        public void Process(Order order)
        {
            foreach (var step in _steps)
            {
                if (step.ShouldProcess(order))
                {
                    step.Process(order);
                }
            }
        }
    }
}
