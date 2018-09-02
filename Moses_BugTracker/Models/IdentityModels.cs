﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Moses_BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }
               
        //Navigational Properties (aka the Relationship)
        //Navigational properties pointing to all the Ticket Children
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public ApplicationUser()
        {
            this.Tickets = new HashSet<Ticket>();
            this.Projects = new HashSet<Project>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Moses_BugTracker.Models.Ticket> Tickets { get; set; }
        public DbSet<Moses_BugTracker.Models.Project> Projects { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketPriority> TicketPriorities { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketStatus> TicketStatuses { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketType> TicketTypes { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketAttachment> TicketAttachments { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketComment> TicketComments { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketHistory> TicketHistories { get; set; }
        public DbSet<Moses_BugTracker.Models.TicketNotification> TicketNotifications { get; set; }
    }
}