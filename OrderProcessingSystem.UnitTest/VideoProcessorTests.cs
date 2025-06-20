using OrderProcessingSystem.Models;
using OrderProcessingSystem.OrderProcessors;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    [TestFixture]
    public class VideoProcessorTests
    {
        private VideoProcessor _processor;
        private string _basePath = "C:\\work\\Task1\\OrderProcessingSystem\\PackingSlips";

        [SetUp]
        public void SetUp()
        {
            _processor = new VideoProcessor();
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
        public void ShouldProcess_ReturnsTrue_ForLearningToSkiVideo()
        {
            var order = new Order { ProductType = ProductType.Video, ProductName = "Learning to Ski" };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsTrue_ForLearningToSkiVideo_CaseInsensitive()
        {
            var order = new Order { ProductType = ProductType.Video, ProductName = "learning to ski" };
            Assert.IsTrue(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForOtherVideo()
        {
            var order = new Order { ProductType = ProductType.Video, ProductName = "Other Video" };
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void ShouldProcess_ReturnsFalse_ForNonVideoProduct()
        {
            var order = new Order { ProductType = ProductType.Book, ProductName = "Learning to Ski" };
            Assert.IsFalse(_processor.ShouldProcess(order));
        }

        [Test]
        public void Process_CreatesPackingSlipFile_WithBonus_AndWritesToConsole()
        {
            var order = new Order { ProductType = ProductType.Video, ProductName = "Learning to Ski" };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            _processor.Process(order);

            string filePath = Path.Combine(_basePath, "Learning to Ski_PackingSlip.txt");
            Assert.IsTrue(File.Exists(filePath), "Packing slip file was not created.");

            string content = File.ReadAllText(filePath);
            StringAssert.Contains("Packing Slip", content);
            StringAssert.Contains("Product: Learning to Ski", content);
            StringAssert.Contains("Bonus: Free 'First Aid' video included.", content);

            var output = sw.ToString();
            StringAssert.Contains("Added free 'First Aid' video to packing slip.", output);
        }

        [Test]
        public void Process_AppendsBonus_IfFileAlreadyExists()
        {
            var order = new Order { ProductType = ProductType.Video, ProductName = "Learning to Ski" };
            string filePath = Path.Combine(_basePath, "Learning to Ski_PackingSlip.txt");

            Directory.CreateDirectory(_basePath);
            File.WriteAllText(filePath, "Packing Slip\nProduct: Learning to Ski\n");

            using var sw = new StringWriter();
            Console.SetOut(sw);

            _processor.Process(order);

            string content = File.ReadAllText(filePath);
            // Should contain the original content and the bonus appended
            StringAssert.StartsWith("Packing Slip\nProduct: Learning to Ski\n", content);
            StringAssert.Contains("Bonus: Free 'First Aid' video included.", content);

            var output = sw.ToString();
            StringAssert.Contains("Added free 'First Aid' video to packing slip.", output);
        }
    }
}