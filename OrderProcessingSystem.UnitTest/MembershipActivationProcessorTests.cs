using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class MembershipActivationProcessorTests
    {
        private MembershipActivationProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new MembershipActivationProcessor();
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForMembershipProductType()
        {
            var order = new Order { ProductType = ProductType.Membership };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForNonMembershipProductType()
        {
            var order = new Order { ProductType = ProductType.Book }; // Use any non-membership type
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_WritesToConsole()
        {
            var order = new Order
            {
                ProductType = ProductType.Membership,
                ProductName = "Gold",
                CustomerEmail = "test@example.com"
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Note: This will actually call EmailService.Send. If you want to avoid sending emails,
            // consider refactoring EmailService to be injectable or mockable.
            _processor.Process(order);

            var output = sw.ToString();
            StringAssert.Contains("Membership activated.", output);
        }
    }
}