using System.Web;
using System.Web.Mvc;
using VinhEdu.App_Start;

namespace VinhEdu
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionFilter());
        }
    }
}
