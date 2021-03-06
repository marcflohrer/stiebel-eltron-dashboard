using Ardalis.Specification;
using stiebel_eltron_apiserver.Core.Entities;

namespace stiebel_eltron_apiserver.Core.Specifications
{
    public class IncompleteItemsSpecification : Specification<ToDoItem>
    {
        public IncompleteItemsSpecification()
        {
            Query.Where(item => !item.IsDone);
        }
    }
}
