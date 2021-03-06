using stiebel_eltron_apiserver.Core;
using stiebel_eltron_apiserver.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace stiebel_eltron_apiserver.Web.Pages.ToDoRazorPage
{
    public class PopulateModel : PageModel
    {
        private readonly IRepository _repository;

        public PopulateModel(IRepository repository)
        {
            _repository = repository;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            RecordsAdded = DatabasePopulator.PopulateDatabase(_repository);
        }
    }
}
