using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Web.App_Start;

namespace Web.Controllers.Attributes
{
    //TODO: Mover isso para o projeto do NHibernate
    public class NHSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            var nhSession = container.GetInstance<ISession>();

            nhSession.BeginTransaction();

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            var nhSession = container.GetInstance<ISession>();

            var hasActiveTransaction = nhSession != null && nhSession.Transaction != null && nhSession.Transaction.IsActive;

            if (hasActiveTransaction)
            {
                nhSession.Transaction.Commit();
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}