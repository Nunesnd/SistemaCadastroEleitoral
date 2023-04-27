using Microsoft.AspNetCore.Mvc.Filters;

namespace SistemaCadastroEleitoral.Infraestrutura.Autenticacao
{
    public class LogadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["adm_sis"]))
            {
                filterContext.HttpContext.Response.Redirect("/login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}