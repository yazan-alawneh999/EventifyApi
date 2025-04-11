using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
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
    public class BuyTicketController : ControllerBase
    {
        private readonly IBuyTicketService _buyTicketService;
        private readonly IAuthRepo _authRepo;


        public BuyTicketController(IBuyTicketService buyTicketService, IAuthRepo authRepo)
        {
            _buyTicketService = buyTicketService;
            _authRepo = authRepo;
        }

        [HttpPost]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "3" })]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicket TicketInfo)
        {
            if (TicketInfo == null)
            {
                return  BadRequest("invalid info");
            }

            // handle not found exceptions - eventId , userId 
            if (!await _authRepo.UserExistsAsync(TicketInfo.t_UserID))
            {
                return NotFound("User not found");
            }
            if (!(_buyTicketService.BuyTicket(TicketInfo) > 0 )){
                return BadRequest("faild creation");
            }



              return Ok(new {message =  "success" }); 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> tickitDetails(int ticket)
        {
            //if (ticket == 0)
            //{
            //    return BadRequest("invalid info");
            //}




            return Ok("success");
        }
    }
}
