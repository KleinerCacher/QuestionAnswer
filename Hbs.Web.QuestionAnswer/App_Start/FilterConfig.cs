using Hbs.Web.QuestionAnswer.Common.Error;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
