using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.SAGA.Commands
{
    public class OrderApproveCommand : CorrelatedBy<Guid>
    {
        public string OrderId { get; set; }
        public Guid CorrelationId { get; set; }
        public bool isApproved { get; set; }
    }
}
