using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.SAGA.Commands
{
    public class InitiateOrderCommand : CorrelatedBy<Guid>
    {
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Guid CorrelationId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
