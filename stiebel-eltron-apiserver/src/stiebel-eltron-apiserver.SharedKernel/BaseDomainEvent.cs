using System;
using MediatR;

namespace stiebel_eltron_apiserver.SharedKernel
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}