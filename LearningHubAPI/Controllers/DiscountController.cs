using LearningHub.Core.Dto;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService) 
        { 
            _discountService = discountService;
        }

        [HttpGet]
        [Route("GetDiscountsByUserID/{userID}")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "3", "2" })]
        public IActionResult GetDiscountsByUserID(decimal userID) 
        {
            if (userID != 0)
            {
                var result = _discountService.GetDiscountsByUserID(userID);
                return Ok(result);

            }
            else
            {
                return BadRequest("Invalid user ID.");
            }
        }

        [HttpGet]
        [Route("GetDiscountsByUserAndCode/{userID}/{Code}")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "3", "2" })]
        public IActionResult GetDiscountsByUserAndCode( decimal userID, String Code) 
        {
            if ( userID != 0 && Code != null) 
            {
                var result = _discountService.GetDiscountsByUserAndCode(userID, Code);
                return Ok(result);

            }
            else 
            {
                return BadRequest("Invalid user ID or Discount Code");
            }
                    
        }


        [HttpGet]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "3", "2" })]
        public IActionResult GetAllDiscounts() { 
            var result = _discountService.GetAllDiscounts();    
            return Ok(result);
        }
    }
}
