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
                // var machinename = ((System.Web.HttpServerUtilityWrapper)((System.Web.HttpContextWrapper)((System.Web.Mvc.Controller)filterContext.Controller).HttpContext).Server).MachineName;
                // var message = filterContext.Exception.Message;

                // $"{DateTime.Now} => No servidor {machinename}, o Usuário: {user}, Login: {login}, IP: {ip}, utilizando a sessão {session} e os cookies: {cookies}, fez um request na Action: {action} da Controller: {controller}, utilizando o browser: {browser}, no sistema operacional: {OS}. OS headers HTTP eram: {headers}, com a querystring { url} utilizando o verbo http { verbo} e obteve a response { response}. a Model anterior era { ModelAnteriorSerializada} e a posterior era { ModelPosteriorSerializada}. A mensafem de erro foi: {message}";

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