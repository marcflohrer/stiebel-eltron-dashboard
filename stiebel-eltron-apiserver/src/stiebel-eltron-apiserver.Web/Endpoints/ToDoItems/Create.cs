using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using stiebel_eltron_apiserver.Core.Entities;
using stiebel_eltron_apiserver.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace stiebel_eltron_apiserver.Web.Endpoints.ToDoItems
{
    public class Create : BaseAsyncEndpoint
    .WithRequest<NewToDoItemRequest>
    .WithResponse<IList<ToDoItemResponse>>
    {
        private readonly IRepository _repository;

        public Create(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("/ToDoItems")]
        [SwaggerOperation(
            Summary = "Creates a new ToDoItem",
            Description = "Creates a new ToDoItem",
            OperationId = "ToDoItem.Create",
            Tags = new[] { "ToDoItemEndpoints" })
        ]
        public override async Task<ActionResult<IList<ToDoItemResponse>>> HandleAsync(NewToDoItemRequest request, CancellationToken cancellationToken)
        {
            var item = new ToDoItem
            {
                Title = request.Title,
                Description = request.Description
            };

            var createdItem = await _repository.AddAsync(item);

            return Ok(createdItem);
        }
    }
}
