using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.NourhanRageb.APIs.Error;
using Store.NourhanRageb.APIs.Errors;

namespace Store.NourhanRageb.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
        }
    }
}
