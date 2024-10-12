using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.NourhanRageb.APIs.Error;
using Store.NourhanRageb.Repository.Data.Contexts;

namespace Store.NourhanRageb.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;
        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // Get: /api/Bugg/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brands = await _context.Brands.FindAsync(100);
            if (brands is null) return NotFound(new ApiErrorResponse(400, "Brands With id : 100 is Not Found"));
            return Ok(brands);
        }

        [HttpGet("servererror")] // Get: /api/Bugg/servererror
        public async Task<IActionResult> GetServerError()
        {
            var brands = await _context.Brands.FindAsync(100);
            var brandTostring = brands.ToString(); // Will Throw Exception (NullReferenceEXception)
            return Ok(brands);
        }

        [HttpGet("badrequest")] // Get: /api/Bugg/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badrequest/{id}")] // Get: /api/Bugg/badrequest/ahmed
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            return Ok();
        }

        [HttpGet("Unauthorized")] // Get: /api/Bugg/Unauthorized
        public async Task<IActionResult> GetUnauthorizedError() // Validation Error
        {
            return Unauthorized(new ApiErrorResponse(400));
        }
    }
}
