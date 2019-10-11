using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;

namespace VinhEdu.App_Start
{
    public class SessionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.Session["UserID"] == null)
            {
                UnitOfWork db = new UnitOfWork();
                User user = null;
                if (HttpContext.Current.User.IsInRole("student"))
                {
                    user = db.UserRepository.FindByStudentID(HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    user = db.UserRepository.FindByEmail(HttpContext.Current.User.Identity.Name);
                }
                HttpContext.Current.Session["UserID"] = user.ID;
            }
            base.OnActionExecuting(filterContext);
        }

    }
}