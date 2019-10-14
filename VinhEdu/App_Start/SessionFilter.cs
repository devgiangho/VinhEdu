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
                var user = db.UserRepository.FindByIdentifier(HttpContext.Current.User.Identity.Name);
                if(user != null)
                {
                    HttpContext.Current.Session["UserID"] = user.ID;
                    HttpContext.Current.Session["Name"] = user.FullName;
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}