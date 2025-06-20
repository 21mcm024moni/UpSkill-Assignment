using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class MembershipUpgradeProcessorTests
    {
        private MembershipUpgradeProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new MembershipUpgradeProcessor();
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForMembershipUpgradeProductType()
        {
            var order = new Order { ProductType = ProductType.MembershipUpgrade };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForNonMembershipUpgradeProductType()
        {
            var order = new Order { ProductType = ProductType.Book }; // Use any non-upgrade type
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_WritesToConsole()
        {
            var order = new Order
            {
                ProductType = ProductType.MembershipUpgrade,
                ProductName = "Platinum",
                CustomerEmail = "test@example.com"
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // This will actually call EmailService.Send. To avoid sending emails, refactor EmailService to be injectable or mockable.
            _processor.Process(order);

            var output = sw.ToString();
            StringAssert.Contains("Membership upgraded.", output);
        }
    }
}