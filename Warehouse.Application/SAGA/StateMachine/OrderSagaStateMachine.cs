using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.SAGA.Commands;

namespace Warehouse.Application.SAGA.StateMachine
{
    public class OrderSagaStateMachine : MassTransitStateMachine<OrderSagaState>
    {
        public Event<InitiateOrderCommand> InitiateOrderEvent { get; }
        public Event<OrderApproveCommand> OrderApproveEvent { get; }
        public Event<OrderApproved> OrderApprovedEvent { get; }
        public Event<OrderDeclined> OrderDeclinedEvent { get; }
        public Event<ResolveAwaitingOrdersCommand> ResolveAwaitingOrdersEvent { get; }


        public State Processing { get; set; }
        public State Resolve { get; set; }
        public State OrderCompleted { get; set; }

        public OrderSagaStateMachine()
        {
            InstanceState(c => c.CurrentState);

            Initially(
                When(InitiateOrderEvent).TransitionTo(Processing),
                When(ResolveAwaitingOrdersEvent).TransitionTo(Resolve),
                When(OrderApproveEvent).TransitionTo(OrderCompleted));

            During(Resolve,
             When(OrderApproveEvent)
             .TransitionTo(OrderCompleted));

            During(OrderCompleted, Processing,                
                When(OrderApprovedEvent)
                .Publish(c =>  new CompleteOrderCommand
                {
                    CorrelationId = c.Data.CorrelationId,
                    ProductId = c.Data.ProductId,
                    Amount = c.Data.Amount
                }).TransitionTo(Final),

                When(OrderDeclinedEvent)                    
                    .TransitionTo(Final)
            );

           
          
        }
    }


    public class OrderSagaState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }

        public int Version { get; set; }
    }
    public class OrderApproved : CorrelatedBy<Guid>
    {
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Guid CorrelationId { get; set; }
    }

    public class OrderDeclined : CorrelatedBy<Guid>
    {
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
