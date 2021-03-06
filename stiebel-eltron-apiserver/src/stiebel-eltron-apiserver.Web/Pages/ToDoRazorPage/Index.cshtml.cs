using System.Collections.Generic;
using System.Threading.Tasks;
using stiebel_eltron_apiserver.Core.Entities;
using stiebel_eltron_apiserver.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace stiebel_eltron_apiserver.Web.Pages.ToDoRazorPage
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public List<ToDoItem> ToDoItems { get; set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            ToDoItems = await _repository.ListAsync<ToDoItem>();
        }
    }
}
