﻿using LearningHub.Core.Dto;
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
        private readonly IEventService _eventService;
        private readonly IReportsService _reportsService;


        public BuyTicketController(IBuyTicketService buyTicketService, IAuthRepo authRepo,IEventService eventService,IReportsService reportsService)
        {
            _buyTicketService = buyTicketService;
            _authRepo = authRepo;
            _eventService = eventService;
            _reportsService = reportsService;
        }

        [HttpPost]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "3" })]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicket TicketInfo)
        {
            int eventID = (int)TicketInfo.t_EventID;
            if (TicketInfo == null)
            {
                return  BadRequest("invalid info");
            }

            if (_eventService.getEventByID(eventID) == null)
            {
                return BadRequest("invalid EventID");
            }


            if (!await _authRepo.UserExistsAsync(TicketInfo.t_UserID))
            {
                return NotFound("User not found");
            }

            if (_buyTicketService.BuyTicket(TicketInfo)==false){
                return BadRequest("faild creation");
            }

            if (_eventService.getEventByID(eventID).Capacity <= _reportsService.GetAttendanceReport(eventID).TOTAL_TICKETS) 
            {
                return BadRequest("The Event is Full");
            }
             return Ok(new {message =  "success" }); 
        }
    }
}
