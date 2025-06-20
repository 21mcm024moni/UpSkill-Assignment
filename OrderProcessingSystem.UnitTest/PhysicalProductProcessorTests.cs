using NUnit.Framework;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;
using System;
using System.IO;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class PhysicalProductProcessorTests
    {
        private PhysicalProductProcessor _processor;
        private string _packingSlipsDir = "PackingSlips";

        [SetUp]
        public void SetUp()
        {
            _processor = new PhysicalProductProcessor();
            if (Directory.Exists(_packingSlipsDir))
                Directory.Delete(_packingSlipsDir, true);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_packingSlipsDir))
                Directory.Delete(_packingSlipsDir, true);
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForPhysicalProduct()
        {
            var order = new Order { ProductType = ProductType.PhysicalProduct };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForNonPhysicalProduct()
        {
            var order = new Order { ProductType = ProductType.Book }; // Use any non-physical type
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_CreatesPackingSlipFile_WithCorrectContent_AndWritesToConsole()
        {
            var order = new Order { ProductType = ProductType.PhysicalProduct, ProductName = "Widget" };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            _processor.Process(order);

            string filePath = Path.Combine(_packingSlipsDir, "Widget_PackingSlip.txt");
            Assert.IsTrue(File.Exists(filePath), "Packing slip file was not created.");

            string content = File.ReadAllText(filePath);
            StringAssert.Contains("Packing Slip", content);
            StringAssert.Contains("Product: Widget", content);
            StringAssert.Contains("Shipping: Standard", content);

            var output = sw.ToString();
            StringAssert.Contains("Packing slip generated for 'Widget'.", output);
        }
    }
}