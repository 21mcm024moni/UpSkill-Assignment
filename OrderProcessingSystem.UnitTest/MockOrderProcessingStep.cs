using NUnit.Framework;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.Services;
using OrderProcessingSystem.Interfaces;
using System;
using System.Collections.Generic;

namespace OrderProcessingSystem.Tests.OrderProcessors
{
    public class MockOrderProcessingStep : IOrderProcessingStep
    {
        public bool ShouldProcessReturnValue { get; set; }
        public bool ShouldProcessCalled { get; private set; }
        public bool ProcessCalled { get; private set; }
        public Order? ProcessedOrder { get; private set; }

        public bool ShouldProcess(Order order)
        {
            ShouldProcessCalled = true;
            return ShouldProcessReturnValue;
        }

        public void Process(Order order)
        {
            ProcessCalled = true;
            ProcessedOrder = order;
        }
    }

    [TestFixture]
    public class OrderProcessorTests
    {
        [Test]
        public void Process_CallsProcessOnRule_WhenShouldProcessIsTrue()
        {
            var mockRule = new MockOrderProcessingStep { ShouldProcessReturnValue = true };
            var processor = new OrderProcessor(new[] { mockRule });
            var order = new Order();

            processor.Process(order);

            Assert.IsTrue(mockRule.ShouldProcessCalled, "ShouldProcess was not called.");
            Assert.IsTrue(mockRule.ProcessCalled, "Process was not called.");
            Assert.AreSame(order, mockRule.ProcessedOrder, "Order passed to Process is not correct.");
        }

        [Test]
        public void Process_DoesNotCallProcessOnRule_WhenShouldProcessIsFalse()
        {
            var mockRule = new MockOrderProcessingStep { ShouldProcessReturnValue = false };
            var processor = new OrderProcessor(new[] { mockRule });
            var order = new Order();

            processor.Process(order);

            Assert.IsTrue(mockRule.ShouldProcessCalled, "ShouldProcess was not called.");
            Assert.IsFalse(mockRule.ProcessCalled, "Process should not have been called.");
        }

        [Test]
        public void Process_CallsProcessOnMultipleRules_IfShouldProcessIsTrue()
        {
            var mockRule1 = new MockOrderProcessingStep { ShouldProcessReturnValue = true };
            var mockRule2 = new MockOrderProcessingStep { ShouldProcessReturnValue = true };
            var processor = new OrderProcessor(new[] { mockRule1, mockRule2 });
            var order = new Order();

            processor.Process(order);

            Assert.IsTrue(mockRule1.ProcessCalled, "Process was not called on rule 1.");
            Assert.IsTrue(mockRule2.ProcessCalled, "Process was not called on rule 2.");
        }
    }
}