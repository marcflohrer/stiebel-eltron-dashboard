using System.Linq;
using System.Threading.Tasks;
using stiebel_eltron_apiserver.Core;
using stiebel_eltron_apiserver.Core.Entities;
using stiebel_eltron_apiserver.SharedKernel.Interfaces;
using stiebel_eltron_apiserver.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace stiebel_eltron_apiserver.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IRepository _repository;

        public ToDoController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = (await _repository.ListAsync<ToDoItem>())
                            .Select(ToDoItemDTO.FromToDoItem);
            return View(items);
        }

        public IActionResult Populate()
        {
            int recordsAdded = DatabasePopulator.PopulateDatabase(_repository);
            return Ok(recordsAdded);
        }
    }
}
