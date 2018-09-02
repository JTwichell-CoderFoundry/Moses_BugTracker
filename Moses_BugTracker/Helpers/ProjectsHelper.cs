using Moses_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Moses_BugTracker.Helpers
{
    public class ProjectsHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Here are the 6 methods included in the Project Helper
        public bool IsUserOnProject(string userid, int projectId)
        {
            var project = db.Projects.Find(projectId);
            return project.Users.Any(u => u.Id == userid);
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            return user.Projects.ToList();
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);
                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);
                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified;
            }
        }

        public ICollection<ApplicationUser> UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }
    }
}