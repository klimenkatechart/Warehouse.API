using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Application.SAGA.Consumers;
using Warehouse.Application.SAGA.StateMachine;

namespace Warehouse.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMassTransit(c =>
            {
                c.AddConsumer<OrderApproveConsumer>();
                c.AddConsumer<CompleteOrderConsumer>();
                c.AddConsumer<InitiateOrderConsumer>();
                c.AddConsumer<ResolveAwaitingOrdersConsumer>();
                c.AddSagaStateMachine<OrderSagaStateMachine, OrderSagaState>(c =>
                {
                    c.UseInMemoryOutbox();
                })
                    .MongoDbRepository("mongodb://localhost:27017", e =>
                    {
                        e.CollectionName = "OrderSaga";
                        e.DatabaseName = "Warehouse";
                    });

                c.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("root");
                        h.Password("example");
                    });

                    cfg.ConfigureEndpoints(context);
                });
                 });
            return services;
        }
        

    }
}
