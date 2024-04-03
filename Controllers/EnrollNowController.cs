using Learning.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.Controllers
{
    public class EnrollNowController : Controller
    {
        LearnDBEntities4 db = new LearnDBEntities4();

        // GET: EnrollNow
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EnrollView()

        {
            List<Cours> course = Session["Course"] as List<Cours>;

            if (course == null)

            {

                course = new List<Cours>();

                Session["Course"] = course;

            }
            return View(course);

        }
        public ActionResult PaidView()
        {
            return View();
        }

       

        public ActionResult Checkout(int id)

        {

            // Retrieve course items from session

            List<Cours> courseItems = Session["Course"] as List<Cours>;

            if (courseItems == null || courseItems.Count == 0)

            {

                // Redirect to an empty course page or display a message

                return RedirectToAction("EmptyCourse");

            }
           

            if (courseItems.Count > 0)
            {
                var course = db.Courses.Where(d => d.CourseId == id).Select(c => c.CourseId).FirstOrDefault();
                var coursename = (db.Courses.Where(d => d.CourseId == id).Select(c => c.CourseName)).FirstOrDefault();
                var coursefees = (db.Courses.Where(d => d.CourseId == id).Select(c => c.CourseFee)).FirstOrDefault();
                
                var buy = new Buy

                {
                    CourseId = course,
                    CourseName = coursename,
                    CourseFee = coursefees,

                    EnrollDate = DateTime.Now,


                    // Set the initial order status

                };
                db.Buys.Add(buy);

                db.SaveChanges();


                Session["Course"] = null;


                return RedirectToAction("PaidView", new { EnrollId = buy.EnrollId });

            }
            else
            {
                return RedirectToAction("EmptyCourse");
            }
        }

        [HttpPost]
        public ActionResult AddToCourse(int courseId, string coursecode, string courseName, string description, decimal courseFee)
        {

            List<Cours> course = Session["Course"] as List<Cours> ?? new List<Cours>();
            if (course.Any(item => item.CourseId == courseId))
            {
                TempData["Message"] = "You are already enrolled this course";
            }
            else
            {
                course.Add(new Cours { CourseId = courseId, CourseCode = coursecode, CourseName = courseName, Description = description, CourseFee = courseFee });
                Session["Course"] = course;

            }
            return RedirectToAction("EnrollView");

        }
        public ActionResult RemoveFromCourse(int courseId)

        {


            List<Cours> course = Session["Course"] as List<Cours>;

            if (course != null)

            {

                course.RemoveAll(item => item.CourseId == courseId);

                Session["Course"] = course;

            }

            return RedirectToAction("EnrollView");

        }

        public ActionResult ClearCourse()

        {


            Session.Remove("Course");

            return Redirect("~/EnrollNow/EnrollView");

        }

    }
}
