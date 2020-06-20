using GörevYöneticisiMulakatTemel.Models;
using GörevYöneticisiMulakatTemel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GörevYöneticisiMulakatTemel.Controllers
{
    public class JobsController : Controller
    {
        private DB_A633FB_gorevtakipEntities1 db = new DB_A633FB_gorevtakipEntities1();

        public static class GlobalVar
        {
            static string _globalValue;
            public static string UserId
            {
                get
                {
                    return _globalValue;
                }
                set
                {
                    _globalValue = value;
                }
            }
        }
        // GET: Jobs
        public ActionResult Index(int? id)
        {
            var G_jobs = db.Jobs.Include(c => c.Users);
            GlobalVar.UserId = id.ToString();
            var G_job = from e in G_jobs
                        where e.Users.UserId == id
                            select e;

            List<JobsVM> P_jobsList = new List<JobsVM>() ;

            foreach (var item in G_job)
            {
                JobsVM P_job = new JobsVM();
                P_job.JobType = item.JobType;
                P_job.JobId = item.JobId;
                P_job.JobComment = item.JobComment;
                P_job.JobDate = Convert.ToDateTime(item.JobDate);
                P_job.JobDefinition = Operations.DateCount(P_job.JobDate, P_job.JobType);
                P_jobsList.Add(P_job);
            }
            return View(P_jobsList.ToList());
        }


        // GET: Jobs/Create
        public ActionResult Create()
        {
            JobsVM P_jobs = new JobsVM();
            P_jobs.getTypeList = P_jobs.getAllList();
            return View(P_jobs);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobsVM G_job)
        {
            if (ModelState.IsValid)
            {
                Jobs P_job = new Jobs();
                DateTime P_date = DateTime.Now ;
                string P_dateS = P_date.Hour + "," + P_date.Minute;
                double P_dateD = Convert.ToDouble(P_dateS);

                P_job.JobDate = G_job.JobDate.AddHours(P_date.Hour).AddMinutes(P_date.Minute);


                P_job.JobType = G_job.JobType;
                P_job.JobComment = G_job.JobComment;
                P_job.UserId = Convert.ToInt32(GlobalVar.UserId);

                db.Jobs.Add(P_job);
                db.SaveChanges();
                return RedirectToAction("Index/"+ GlobalVar.UserId);
            }
            G_job.getTypeList = G_job.getAllList();

            return View(G_job);
        }
        // userid olmazsa logine yönlendir

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs G_job = db.Jobs.Find(id);

            if (G_job == null)
            {
                return HttpNotFound();
            }

            JobsVM P_job = new JobsVM();
            P_job.JobId = G_job.JobId;
            P_job.JobType = G_job.JobType;
            P_job.JobComment = G_job.JobComment;
            P_job.JobDate = G_job.JobDate;
            P_job.getTypeList = P_job.getAllList();



            return View(P_job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( JobsVM G_job)
        {
            if (ModelState.IsValid)
            {
                Jobs P_job = new Jobs();
                P_job.JobId = G_job.JobId;
                P_job.JobDate = G_job.JobDate;
                P_job.JobType = G_job.JobType;
                P_job.JobComment = G_job.JobComment;
                P_job.UserId = Convert.ToInt32(GlobalVar.UserId);

                db.Entry(P_job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + GlobalVar.UserId);
            }
            return View(G_job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs G_job = db.Jobs.Find(id);

            JobsVM P_job = new JobsVM();
            P_job.JobId = G_job.JobId;
            P_job.JobType = G_job.JobType;
            P_job.JobComment = G_job.JobComment;
            P_job.JobDate = G_job.JobDate;
            if (G_job == null)
            {
                return HttpNotFound();
            }
            return View(P_job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobs G_job = db.Jobs.Find(id);
            db.Jobs.Remove(G_job);
            db.SaveChanges();
            return RedirectToAction("Index/" + GlobalVar.UserId);
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
