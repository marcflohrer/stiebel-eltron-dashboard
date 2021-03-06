using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using stiebel_eltron_apiserver.Core.Entities;

namespace stiebel_eltron_apiserver.Core.Interfaces
{
    public interface IToDoItemSearchService
    {
        Task<Result<ToDoItem>> GetNextIncompleteItemAsync();
        Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(string searchString);
    }
}
