using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace ContactApplication.CustomAttributes
{
    public class CustomExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                string loggerName = string.Format("Controller :{0}, ActionMethod: {1}", controller, action);

                log4net.LogManager.GetLogger(loggerName).Error(filterContext.Exception.Message.ToString());
            }
        }
    }
}