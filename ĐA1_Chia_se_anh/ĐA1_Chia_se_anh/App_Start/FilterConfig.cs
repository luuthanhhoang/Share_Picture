using System.Web;
using System.Web.Mvc;

namespace ĐA1_Chia_se_anh
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
