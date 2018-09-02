﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moses_BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public int TicketId { get; set; }

        public string UserId { get; set; }

        //Navigational Properties (aka the Relationship)
        public virtual Ticket Ticket { get; set; }
    }
}