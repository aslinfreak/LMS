using Learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.Controllers
{
   
    public class BuyController : Controller
    {
        private LearnDBEntities4 db = new LearnDBEntities4();
        // GET: Buy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EnrollList()
        {
            return View(db.Buys.ToList());
        }
    }
}