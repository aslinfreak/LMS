using Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Learning.Controllers
{
    public class AccountController : Controller
    {
        LearnDBEntities4 db = new LearnDBEntities4();
        // GET: Account
        public ActionResult Index()
        {
            if (Session["IsLoggedIn"] == null || (bool)Session["IsLoggedIn"]==false)
            {
                return HttpNotFound();
            }
            return View();
            
        }
        [HandleError]
        public ActionResult StudentRegisters()
        {
            var studentRegisters = db.StudentRegisters.Select(u => new UserRegistrationViewModel
            {

                StudentName = u.StudentName,
                Email = u.Email,
                Gender = u.Gender,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
            }).ToList();
            return View(studentRegisters);
        }
        [HttpGet]
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(StudentRegister ur)
        {
            if (ModelState.IsValid)
            {
                if (db.StudentRegisters.Any(x => x.Email == ur.Email))
                {
                    ViewBag.Message = "Email already registered";
                }
                else
                {
                    db.StudentRegisters.Add(ur);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registration Successful')</script>");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(MyLogin l)
        {
             var student = db.StudentRegisters.SingleOrDefault(m => m.Email == l.Email && m.Password == l.Password);
            var admin = db.AdminRegisters.SingleOrDefault(m => m.Email == l.Email && m.Password == l.Password);
            if (student != null)
            {
                Session["IsLoggedIn"] = true;
                Session["IsUser"] = true;
                Session["StudentName"] = student.StudentName;

                return RedirectToAction("CourseView", "Courses");
            }
            else if (admin != null)
            {
                Session["IsLoggedIn"] = true;
                Session["IsAdmin"] = true;
                Session["StudentName"] = admin.AdminName;

                return RedirectToAction("Index", "Courses");

            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Credentials";
                return View();

            }
        }
        
        public ActionResult Logout()
        {
           Session["IsLoggedIn"] = false;
           Session ["IsUser"] = false;
           Session["IsAdmin"] = false;

            return RedirectToAction("Index", "Home");
}
}
}
