using Microsoft.AspNetCore.Mvc.Filters;
using NETCore_Demo_StajProject.Views;
using Microsoft.AspNetCore.Mvc;
namespace NETCore_Demo_StajProject.App_Filter
{
    public class LoginFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
            var musteriSesion = ProgramUtility.GetMusteri(context.HttpContext); 
            var personelSesion = ProgramUtility.GetPersonel(context.HttpContext);

            if (musteriSesion == null && personelSesion==null)
            {
                context.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary { { "controller", "Home" }, { "action", "Giris" } });
                base.OnActionExecuting(context);
               
            }

     
        }
    }
}
