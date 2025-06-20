using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class CommissionProcessorTests
    {
        private CommissionProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new CommissionProcessor();
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForPhysicalProduct()
        {
            var order = new Order { ProductType = ProductType.PhysicalProduct };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForBook()
        {
            var order = new Order { ProductType = ProductType.Book };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForOtherProductTypes()
        {
            var order = new Order { ProductType = ProductType.Video }; // Replace with a valid non-commission type if needed
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_WritesExpectedMessageToConsole()
        {
            var order = new Order { Agent = "John Doe" };
            using var sw = new StringWriter();
            Console.SetOut(sw);

            _processor.Process(order);

            var output = sw.ToString().Trim();
            Assert.AreEqual("Commission payment generated to the agent John Doe.", output);
        }
    }
}