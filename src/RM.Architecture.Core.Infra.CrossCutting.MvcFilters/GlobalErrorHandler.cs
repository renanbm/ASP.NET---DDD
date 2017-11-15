using System.Web.Mvc;

namespace RM.Architecture.Core.Infra.CrossCutting.MvcFilters
{
    public class GlobalErrorHandler : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // LOG Auditoria
            // Tal usuário gravou X informação na modal Y

            if (filterContext.Exception != null)
            {
                // Manipular a EX
                // Injetar alguma LIB de tratamento de erro
                // => Gravar log do erro no BD
                // => Email para o admin
                // => Retornar cod de erro amigavel

                // SEMPRE DE FORMA ASYNC
            }

            base.OnActionExecuted(filterContext);
        }
    }
}