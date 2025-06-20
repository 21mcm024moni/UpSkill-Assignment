using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class BookProcessorTests
    {
        private BookProcessor _processor;
        private string _basePath;

        [SetUp]
        public void SetUp()
        {
            _processor = new BookProcessor();
            _basePath = "C:\\work\\Task1\\OrderProcessingSystem\\PackingSlips";
            if (Directory.Exists(_basePath))
                Directory.Delete(_basePath, true);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_basePath))
                Directory.Delete(_basePath, true);
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForBookProductType()
        {
            var order = new Order { ProductType = ProductType.Book };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForNonBookProductType()
        {
            var order = new Order { ProductType = ProductType.Video };
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_CreatesRoyaltySlipFile_WithCorrectContent()
        {
            var order = new Order { ProductType = ProductType.Book, ProductName = "TestBook" };
            _processor.Process(order);

            string filePath = Path.Combine(_basePath, "TestBook_RoyaltySlip.txt");
            Assert.IsTrue(File.Exists(filePath), "Royalty slip file was not created.");

            string content = File.ReadAllText(filePath);
            StringAssert.Contains("Duplicate Packing Slip", content);
            StringAssert.Contains("Product: TestBook", content);
            StringAssert.Contains("Sent to: Royalty Department", content);
        }
    }
}