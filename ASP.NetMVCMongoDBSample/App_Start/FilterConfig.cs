using System.Web;
using System.Web.Mvc;

namespace ASP.NetMVCMongoDBSample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
