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
                var currentconfig = db.ConfigRepository.GetAll().Where(z => z.IsActive == true).FirstOrDefault();
                var setting = db.context.Settings.FirstOrDefault().Semmester.GetDisplayName();
                if(user != null)
                {
                    HttpContext.Current.Session["UserID"] = user.ID;
                    HttpContext.Current.Session["Name"] = user.FullName;
                    HttpContext.Current.Session["ConfigID"] = currentconfig.ID;
                    if(user.SubjectID != null)
                    {
                        HttpContext.Current.Session["SubjectID"] = user.SubjectID;
                        HttpContext.Current.Session["Semester"] = setting;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}