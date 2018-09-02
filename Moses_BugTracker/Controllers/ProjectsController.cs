using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Moses_BugTracker.Helpers;
using Moses_BugTracker.Models;

namespace Moses_BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();

        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            var projects = new List<Project>();
            var userId = User.Identity.GetUserId();

            if (User.IsInRole("ADMINISTRATOR"))
            {
                projects = db.Projects.ToList();
            }
            else if (User.IsInRole("PROJECTMANAGER") || User.IsInRole("DEVELOPER") || User.IsInRole("SUBMITTER"))
            {
                projects = projHelper.ListUserProjects(userId).ToList();
            }
            return View(projects);
        }

        [Authorize(Roles = "PROJECTMANAGER,ADMINISTRATOR")]
        public ActionResult AllIndex()
        {
            return View("Index", db.Projects.ToList());
        }

        [Authorize(Roles = "PROJECTMANAGER,ADMINISTRATOR")]
        public ActionResult Assign()
        {         
            ViewBag.AssignableUser = new SelectList(db.Users, "Id", "Email");
            ViewBag.AssignableUsers = new MultiSelectList(db.Users, "Id", "Email");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "PROJECTMANAGER,ADMINISTRATOR")]
        public ActionResult Assign(string assignableUser, List<string> assignableUsers)
        {
            if(string.IsNullOrEmpty(assignableUser) || assignableUsers == null)
            {
                //If the user failed to select either a single user from the SelectList
                //or did not choose any from the Multi Select List we have to tell them
                //they made a mistake and need to try again.

                //First let's add an error that will display in the general Validation Message
                ModelState.AddModelError("", "Please make all the appropriate selections.");

                //Then we need to determine which piece of data is actually missing
                if(string.IsNullOrEmpty(assignableUser))
                {
                    ModelState.AddModelError("AssignableUser", "You must select a user");
                }
                
                if (assignableUsers == null)
                {
                    ModelState.AddModelError("AssignableUsers", "You must select at least one user");
                }

                ViewBag.AssignableUser = new SelectList(db.Users, "Id", "Email");
                ViewBag.AssignableUsers = new MultiSelectList(db.Users, "Id", "Email");

                return View();
            }

            //Save the incoming UserId Or the incoming list of Id's

            return RedirectToAction("Index");         
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles ="ADMINISTRATOR,PROJECTMANAGER")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "ADMINISTRATOR,PROJECTMANAGER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
