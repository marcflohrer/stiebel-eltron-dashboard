using Microsoft.AspNetCore.Mvc;

namespace stiebel_eltron_apiserver.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
