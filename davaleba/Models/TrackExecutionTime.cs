using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace davaleba.Models
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.RouteData.Values["Controller"].ToString() +
                "->" + filterContext.RouteData.Values["Action"].ToString() + "->" +
                filterContext.Exception.Message + " \t-" +
                DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
            LogExecutionTime("------------------------------------------");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = "n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                "->" + filterContext.ActionDescriptor.ActionName + "-> onActionExecuting \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = "n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                "->" + filterContext.ActionDescriptor.ActionName + "-> onActionExecuted \t" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = filterContext.RouteData.Values["Controller"].ToString() +
                "->" + filterContext.RouteData.Values["Action"].ToString() + "-> OnResultExecuting \t-" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = filterContext.RouteData.Values["Controller"].ToString() +
                "->" + filterContext.RouteData.Values["Action"].ToString() + "-> OnResultExecuted \t-" + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        private void LogExecutionTime(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/data.txt"), data);
        }

    }
}