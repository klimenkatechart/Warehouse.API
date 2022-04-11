using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.SAGA.Commands
{
    public class CompleteOrderCommand : CorrelatedBy<Guid>
    {
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
