using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.Services;

namespace OrderProcessingSystem.OrderProcessors
{
    public class MembershipUpgradeProcessor : IOrderProcessingStep
    {
        public bool ShouldProcess(Order order) => order.ProductType == ProductType.MembershipUpgrade;

        public void Process(Order order)
        {
            Console.WriteLine("Membership upgraded.");
            EmailService.Send(order.CustomerEmail, "Membership Upgraded", $"Your membership has been upgraded to {order.ProductName}.");
        }
    }
}
