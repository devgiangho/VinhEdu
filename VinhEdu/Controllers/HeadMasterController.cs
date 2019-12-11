using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Repository;
using VinhEdu.ViewModels;
using VinhEdu.Models;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "headmaster")]
    public class HeadMasterController : Controller
    {
        // GET: HeadMaster
        [Authorize(Roles = "headmaster")]
        public ActionResult Index()
        {
            UnitOfWork db = new UnitOfWork();
            return View();
        }
    }
}