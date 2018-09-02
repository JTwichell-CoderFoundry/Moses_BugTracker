﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moses_BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime Changed { get; set; }

        public string UserId { get; set; }

        //Navigational Properties (aka the Relationship)
        public virtual Ticket Ticket { get; set; }
    }
}