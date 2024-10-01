using ECommerceIAbstract;
using ECommerceShared.CoreModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
   
     [ApiController]
     [Route("api/[controller]")]
    public class ECommerceController : ControllerBase
    {
        private readonly IBusinessRepo _businessRepo;
        public ECommerceController(IBusinessRepo businessRepo)
        {
            _businessRepo = businessRepo;
        }

        [HttpPost("GetLatestOrder")]
        public async Task<IActionResult> GetLatestOrder([FromBody] CustomerRequestModel request)
        {
            if (string.IsNullOrEmpty(request.User) || string.IsNullOrEmpty(request.CustomerId))
                return BadRequest("Invalid request parameters.");

            var result = await _businessRepo.GetLatestOrder(request);

            if (result == null)
                return NotFound("Customer not found or email mismatch.");

            return Ok(result);
        }
    }
}
