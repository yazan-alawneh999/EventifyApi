﻿using LearningHub.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Repository
{
    public interface IBuyTicketRepository
    {
        public bool BuyTicket(BuyTicket TicketInfo, String qrText); 
    }
}
