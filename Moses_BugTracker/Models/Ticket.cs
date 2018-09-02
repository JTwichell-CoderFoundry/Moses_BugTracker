using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moses_BugTracker.Models
{
    public class Ticket
    {
        //Primary Key for the Tickets model
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public int ProjectId { get; set; }

        public int TicketTypeId { get; set; }

        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public string OwnerUserId { get; set; }

        public string AssignedToUserId { get; set; }

        //Navigational Properties (aka the Relationship)
        //Navigational properties pointing to all the Ticket Children
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //Navigational properties pointing to all the Ticket Parents
        public Project Project { get; set; }
        public TicketType TicketType { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public ApplicationUser OwnerUser { get; set; }
        public ApplicationUser AssignedToUser { get; set; }

        //Initialize the navigational properties in the Constructor
        public Ticket()
        {
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketComments = new HashSet<TicketComment>();
            this.TicketHistories = new HashSet<TicketHistory>();
            this.TicketNotifications = new HashSet<TicketNotification>();
        }
    }
}