using stiebel_eltron_apiserver.Core.Entities;
using stiebel_eltron_apiserver.SharedKernel;

namespace stiebel_eltron_apiserver.Core.Events
{
    public class ToDoItemCompletedEvent : BaseDomainEvent
    {
        public ToDoItem CompletedItem { get; set; }

        public ToDoItemCompletedEvent(ToDoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}