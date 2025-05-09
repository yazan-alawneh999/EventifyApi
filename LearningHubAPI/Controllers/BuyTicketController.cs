﻿using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LearningHub.Infra.Exceptions;

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



        [HttpGet]
        [Route("GetAllTicketsByUserID/{ID}")]
        [Authorize]
        public async Task<IActionResult> GetAllTicketsByUserID(decimal ID)
        {
            if (ID != 0 && ID > 0) 
            {
                var result = _buyTicketService.GetAllTicketsByUserId(ID);
                return Ok(result);
            }
            else { return BadRequest("User ID not Valid"); }
        }
        
        [HttpGet]
        [Route("GetAllTicketsByTicketId/{ticketID}")]
        public IActionResult GetTicketsByTicketId(decimal ticketID)
        {

            if (ticketID > 0)
            {
                var result =_buyTicketService.GetTicketsByTicketId(ticketID);
                return Ok(result);
            }
            else {
                return BadRequest("Ticket id not valid");
            }
        }
        
        [HttpPost("by-qrcode")]
        public async Task<IActionResult> CheckInByQRCode([FromBody] string qrCode)
        {
            try
            {
                await _buyTicketService.CheckInByQRCodeAsync(qrCode);
                return Ok("Check-in successful");
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TicketAlreadyCheckedInException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
